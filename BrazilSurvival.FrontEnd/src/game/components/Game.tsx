import { useReducer, useEffect, useContext, useState } from "react";
import Challenge from "../types/Challenge";
import ChallengeConsequences from "./ChallengeConsequences";
import ChallengeQuestionary from "./ChallengeQuestionary";
import GameOver from "./GameOver";
import PlayerStatsElements from "./PlayerStatsElements";
import { GameEventAction } from "../types/GameEventAction";
import { GameState } from "../types/GameState";
import { GameServiceContext } from "../../contexts";
import { NextChallengeAnswer } from "../types/NextChallengeAnswer";
import PlayerStats from "./PlayerStats";

const ANSWER_NOT_SELECTED = -1;

const initialState: GameState = {
  gameIsLoading: true,
  gameIsOver: false,
  challenges: [],
  currentChallenge: {
    id: 0,
    title: "",
    options: []
  },
  selectedAnswer: ANSWER_NOT_SELECTED,
  playerStats: {
    health: 0,
    money: 0,
    power: 0,
  },
  score: 0,
  nextChallengeResult: {
    isGameOver: false,
    newPlayerStats: {
      health: 0,
      money: 0,
      power: 0,
    },
    answer: "",
    consequence: "",
    effect: {
      health: 0,
      money: 0,
      power: 0,
    }
  }
};

/**
   * Processa um novo estado depois que um `dispatchGameEvent()` foi disparado.
   * @param state Estado atual do jogo
   * @param action Evento discipado
   * @returns Novo estado atualizado
   */
function handleGameState(state: GameState, action: GameEventAction): GameState {
  switch (action.type) {
    case "on-game-started":
      return {
        ...state,
        currentChallenge: action.payload.challenges?.pop() as Challenge,
        challenges: action.payload.challenges as Challenge[],
        gameIsLoading: false,
        gameIsOver: false,
        selectedAnswer: ANSWER_NOT_SELECTED,
        playerStats: action.payload.playerStats as PlayerStats
      }
    case "on-answer-selected":
      const newSelectedAnswer = action.payload.selectedAnswer;

      if (newSelectedAnswer === undefined) return { ...state };
      if (newSelectedAnswer < -1) return { ...state };

      return {
        ...state,
        selectedAnswer: newSelectedAnswer,
        nextChallengeResult: action.payload.nextChallengeResult as NextChallengeAnswer,
        playerStats: action.payload.nextChallengeResult?.newPlayerStats as PlayerStats
      }

    case "on-next-challenge":
      var newChallenges = [...state.challenges];

      if (newChallenges.length === 0) {
        newChallenges = [...state.nextChallengeResult.newChallenges as Challenge[]];
      }

      const nextChallenge = newChallenges.pop() as Challenge;
      const newScore = state.score + 1;

      return {
        ...state,
        currentChallenge: nextChallenge,
        challenges: newChallenges,
        selectedAnswer: ANSWER_NOT_SELECTED,
        score: newScore
      }

    case "on-game-over":
      return {
        ...state,
        gameIsOver: true,
        gameIsLoading: false,
      }

    default: return { ...state };
  }
}

export default function Game() {
  const [gameState, dispatchGameEvent] = useReducer(handleGameState, initialState);
  const gameService = useContext(GameServiceContext);
  const [error, setError] = useState<Error | undefined>();

  useEffect(() => {
    initializeGameState();
  }, []);

  /**
   * Inicializa o estado do jogo.
   * O estado poderia ser inicializado utilizando o `useReducer`, 
   * porém, como é preciso fazer uma chamada à API a função precisa ser asíncrona, um caso que o `useReducer` não cobre.
   */
  async function initializeGameState() {
    try {
      const response = await gameService.startGame();

      const newGameState: Partial<GameState> = {
        challenges: response.challenges,
        playerStats: response.playerStats,
      };

      dispatchGameEvent({
        type: "on-game-started",
        payload: newGameState
      });
    } catch (err) {
      setError(err as Error);
    }
  }

  async function handleOnNextChallenge() {
    if (gameState.nextChallengeResult.isGameOver) {
      dispatchGameEvent({
        type: "on-game-over",
        payload: {}
      });
      return;
    }

    dispatchGameEvent({
      type: "on-next-challenge",
      payload: {
        challenges: gameState.nextChallengeResult.newChallenges as Challenge[]
      }
    });

  }

  async function handleOnAnswerSelected(selectedOption: number) {
    try {
      const nextChallengeAnswer = await gameService.nextChallenge(gameState.playerStats, gameState.currentChallenge.id, selectedOption, gameState.challenges.length === 0);

      dispatchGameEvent({
        type: "on-answer-selected",
        payload: {
          selectedAnswer: selectedOption,
          nextChallengeResult: nextChallengeAnswer,
        }
      });

    } catch (err) {
      setError(err as Error);
    }
  }

  var component;
  var title = "Brazil Survival";

  if (error) {
    title = "Um erro ocorreu";
    component = (
      <div>
        <p>Deseja tentar novamente?</p>
        <button onClick={() => initializeGameState()}>Sim</button>
      </div>
    );

  } else if (gameState.gameIsOver) {
    title = "Game Over"
    component = (
      <GameOver score={gameState.score} restartGame={() => initializeGameState()} />
    );

  } else if (gameState.gameIsLoading) {
    component = <div className="loading-challenges"><h1>Carregando desafios...</h1></div>

  } else {
    component = (
      <div className="game-body">
        {
          gameState.selectedAnswer === ANSWER_NOT_SELECTED ?
            <ChallengeQuestionary
              challenge={gameState.currentChallenge}
              onAnswerSelected={handleOnAnswerSelected} /> :
            <ChallengeConsequences
              answerChallengeResult={gameState.nextChallengeResult}
              onNextChallenge={handleOnNextChallenge} />
        }
      </div>
    );
  }

  return (
    <div className="game">
      <h2 className="title">{title}</h2>
      {
        component
      }
      <PlayerStatsElements playerStats={gameState.playerStats} />
    </div>
  );
}