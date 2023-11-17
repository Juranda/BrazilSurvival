import { useEffect, useReducer, useState } from "react";
import PlayerStatsElements from "./components/PlayerStatsElements";
import PlayerStats from "./components/PlayerStats";
import StaticDatabase from "./static-database";
import Challenge from "./models/Challenge";
import ChallengeQuestionary from "./components/ChallengeQuestionary";
import ChallengeConsequences from "./components/ChallengeConsequences";
import GameOver from "./components/GameOver";
import AnswerEffect from "./models/AnswerEffect";
import ChallengeOption from "./models/ChallengeOption";

const ANSWER_NOT_SELECTED = -1;

interface GameState {
  gameIsOver: boolean,
  challenges: Challenge[],
  currentChallenge: Challenge,
  selectedAnswer: number,
  playerStats: PlayerStats
}

type GameEventPayload = Partial<GameState>;

interface GameEventAction {
  type: "on-next-challenge" | "on-answer-selected" | "on-game-over" | ""
  payload: GameEventPayload 
}

export default function App() {
  const [isGameOver, setGameOver] = useState(false);
  const [challenges, setChallenges] = useState<Challenge[]>([]);
  const [challenge, setChallenge] = useState<Challenge>({title: "", options: []});
  const [hasChallenges, setHasChallenges] = useState(false);
  const [selectedAnswer, setSelectedAnswer] = useState(ANSWER_NOT_SELECTED);
  const [playerStats, setPlayerStats] = useState<PlayerStats>({
    vida: 8,
    dinheiro: 5,
    poder: 3,
  });

  const [state, dispatchGameEvent] = useReducer(handleGameState, {
    gameIsOver: false,
    challenges: [],
    currentChallenge: {
      title: "",
      options: []
    },
    selectedAnswer: ANSWER_NOT_SELECTED,
    playerStats: {
      vida: 8,
      dinheiro: 5,
      poder: 3,
    }
  });

  useEffect(() => {
    dispatchGameEvent({
      type: "on-next-challenge",
      payload: { }
    });

  }, []);

  /**
   * 
   * @param state Estado atual do jogo
   * @param action Evento discipado
   * @returns Novo estado atualizado
   */
  function handleGameState(state: GameState, action: GameEventAction): GameState {
    switch (action.type) {
      case "on-game-over":
          return {
            ...state,
            gameIsOver: true
          };
      case "on-next-challenge":
        if(action.payload.challenges === undefined) return { ...state };

          var newChallenges: Challenge[];
          var newChallenge: Challenge;

        if(action.payload.challenges.length === 0) {
          // Não existem desafios, então, busque mais do servidor
          fetchRandomChallenges()
          .then(challenges => {
            newChallenges = challenges;
            const newChallengeOrUndefined = newChallenges.pop();

            if(newChallengeOrUndefined === undefined) return;

            newChallenge = newChallengeOrUndefined;
          });
        }

        newChallenges = [...action.payload.challenges];
        const newChallengeOrUndefined = newChallenges.pop();
        if(newChallengeOrUndefined === undefined) return { ...state };

        newChallenge = newChallengeOrUndefined;

        return {
          ...state,
          challenges: newChallenges,
          currentChallenge: newChallenge
        };

      case "on-answer-selected":
        if(action.payload.selectedAnswer === undefined) return { ...state };

        return {
          ...state,
          selectedAnswer: action.payload.selectedAnswer
        };
      default:
        break;
    }


    return { ...state };
  }

  // async function setNewChallenges() {
  //   setHasChallenges(false);

  //   const newChallenges = await fetchRandomChallenges();
  //   const newChallenge = newChallenges.pop();

  //   if(newChallenge === undefined) {
  //     setGameOver(true);
  //     return;
  //   }

  //   setChallenges(newChallenges);
  //   setChallenge(newChallenge);
  //   setHasChallenges(true);
  // }

  async function fetchRandomChallenges() {
    var newChallenges: Challenge[] = [];
    try {
      const response = await fetch('http://localhost:5079/challenges');
      newChallenges = response.status === 200 ? await response.json() : await StaticDatabase.getRandomChallenges()
    } catch (error) {
      newChallenges = await StaticDatabase.getRandomChallenges();
    }

    return newChallenges;
  }

  function onAnswerSelected(index: number) {
    const effect = getEffectFromChallengeOption(challenge.options[index]);

    updatePlayerStats(effect);
    setSelectedAnswer(index);
  }

  function updatePlayerStats(effect: AnswerEffect) {
    setPlayerStats((prev) => ({
      vida: prev.vida + (effect.health || 0),
      dinheiro: prev.dinheiro + (effect.money || 0),
      poder: prev.poder + (effect.power || 0),
    }));
  }

  function onNextChallenge() {
    if(playerStats.vida <= 0 || playerStats.dinheiro <= 0 || playerStats.poder <= 0) {
      setGameOver(true);
      return;
    }

    setSelectedAnswer(ANSWER_NOT_SELECTED);
    
    const lastingChallenges = [...challenges];
    const nextChallenge = lastingChallenges.pop();

    if(nextChallenge === undefined) {
      return;
    }

    setChallenges(lastingChallenges);
    setChallenge(nextChallenge)
  }

  function getEffectFromChallengeOption(option: ChallengeOption) {
    const { health, money, power } = option;
    return { health, money, power };
  }

  return (
    <div className="game">
      <h2 className="title">Brazil Survival</h2>
      <div className="game-body">
        {
          state.gameIsOver ? 
          <GameOver/> : <>
            {
              selectedAnswer === ANSWER_NOT_SELECTED ?
              <ChallengeQuestionary challenge={challenge} onAnswerSelected={onAnswerSelected}/> : 
              <ChallengeConsequences 
                answer={challenge.options[selectedAnswer].answer} 
                consequence={challenge.options[selectedAnswer].consequence} 
                effect={getEffectFromChallengeOption(challenge.options[selectedAnswer])} 
                onNextChallenge={onNextChallenge}/>
            }
            </>
        }
      </div>
      <PlayerStatsElements playerStats={playerStats}/>
    </div>
  );
}