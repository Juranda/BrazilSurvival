import { useEffect, useState } from "react";
import PlayerStatsElements from "./components/PlayerStatsElements";
import PlayerStats from "./components/PlayerStats";
import StaticDatabase from "./static-database";
import Challenge from "./models/Challenge";
import ChallengeQuestionary from "./components/ChallengeQuestionary";
import ChallengeConsequences from "./components/ChallengeConsequences";
import GameOver from "./components/GameOver";
import AnswerEffect from "./models/AnswerEffect";

const NOT_SELECTED_ANSWER = -1;

export default function App() {
  const [isGameOver, setGameOver] = useState(false);
  const [challenges, setChallenges] = useState<Challenge[]>([]);
  const [challenge, setChallenge] = useState<Challenge>({title: "", options: []});
  const [selectedAnswer, setSelectedAnswer] = useState(NOT_SELECTED_ANSWER);
  const [playerStats, setPlayerStats] = useState<PlayerStats>({
    vida: 8,
    dinheiro: 5,
    poder: 3,
  });

  useEffect(() => {
    fetchRandomChallenges();
  }, []);


  async function fetchRandomChallenges() {
    const newChallenges = await StaticDatabase.getRandomChallenges();
    const newChallenge = newChallenges.pop();

    if(newChallenge === undefined) {
      setGameOver(true);
      return;
    }

    setChallenge(newChallenge);
    setChallenges(newChallenges);
  }

  function onAnswerSelected(index: number) {
    updatePlayerStats(effect);
    setSelectedAnswer(index);
  }

  function updatePlayerStats(effect: AnswerEffect) {
    setPlayerStats((prev) => ({
      vida: prev.vida + (effect.vida || 0),
      dinheiro: prev.dinheiro + (effect.dinheiro || 0),
      poder: prev.poder + (effect.poder || 0),
    }));
  }

  function onNextChallenge() {
    if(playerStats.vida <= 0 || playerStats.dinheiro <= 0 || playerStats.poder <= 0) {
      setGameOver(true);
      return;
    }

    const lastingChallenges = [...challenges];
    const nextChallenge = lastingChallenges.pop();
    setSelectedAnswer(NOT_SELECTED_ANSWER);
    
    if(nextChallenge !== undefined) {
      
      setChallenges(lastingChallenges);
      setChallenge(nextChallenge);
      
      return;
    }
    
    fetchRandomChallenges();
  }

  const effect: AnswerEffect = {
    vida: (challenge.options[selectedAnswer]?.health || 0),
    dinheiro: (challenge.options[selectedAnswer]?.money || 0),
    poder: (challenge.options[selectedAnswer]?.power || 0),
  }
  

  return (
    <div className="game">
      <h2 className="title">Brazil Survival</h2>
      <div className="game-body">
        {
          isGameOver ? 
          <GameOver/> : 
          <>
            {
              selectedAnswer === NOT_SELECTED_ANSWER ?
              <ChallengeQuestionary challenge={challenge} onAnswerSelected={onAnswerSelected}/> : 
              <ChallengeConsequences 
                answer={challenge.options[selectedAnswer].answer} 
                consequence={challenge.options[selectedAnswer].consequence} 
                effect={effect} 
                onNextChallenge={onNextChallenge}/>
            }
          </>
        }
      </div>
      <PlayerStatsElements playerStats={playerStats}/>
    </div>
  );
}