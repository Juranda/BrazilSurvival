import { useEffect, useState } from "react";
import AnswerOption from "./components/AnswerOption";
import PlayerStatsElements from "./components/PlayerStatsElements";
import PlayerStats from "./components/PlayerStats";
import IMAGES from "./images";
import AnswerEffect from "./models/AnswerEffect";
import StaticDatabase from "./static-database";
import Challenge from "./models/Challenge";

export default function App() {
  const [isGameOver, setGameOver] = useState(false);
  const [challenges, setChallenges] = useState<Challenge[]>([]);
  const [challenge, setChallenge] = useState<Challenge>({title: "", options: []});
  const [selectedAnswer, setSelectedAnswer] = useState(-1);
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
    setSelectedAnswer(index);
    updatePlayerStats(effect);
  }

  function updatePlayerStats(answerEffect: AnswerEffect) {

    setPlayerStats((prev) => ({
      vida: prev.vida + (answerEffect.vida || 0),
      dinheiro: prev.dinheiro + (answerEffect.dinheiro || 0),
      poder: prev.poder + (answerEffect.poder || 0),
    }));
  }

  function onNextChallenge() {

    console.log(playerStats);
    if(playerStats.vida <= 0 && playerStats.dinheiro <= 0 && playerStats.poder <= 0) {
      setGameOver(true);
      return;
    }

    const lastingChallenges = [...challenges];
    const nextChallenge = lastingChallenges.pop();
    setSelectedAnswer(-1);
    
    if(nextChallenge !== undefined) {
      
      setChallenges(lastingChallenges);
      setChallenge(nextChallenge);
      
      return;
    }
    
    fetchRandomChallenges();
  }

  var effect = {};

  if(selectedAnswer !== -1) {
    effect = { }
  }

  return (
    <div className="game">
      <h2 className="title">Brazil Survival</h2>
      <div className="game-body">
        {
          isGameOver ? 
          <h2>Você perdeu!</h2> : 
          <>
            {
              selectedAnswer === -1 ?
              <>
                <h4 className="challenge-title">{challenge.title}</h4>
                <div className="answer-options">
                  {
                    challenge.options.map((v, i) => <AnswerOption key={i} onAnswerSelected={() => onAnswerSelected(i)} text={`${i + 1}. ${v.action}`}/>)
                  }
                </div> 
              </> : 
              <>
                <div className="challenge-consequenses">
                  <p className="answer">{challenge.options[selectedAnswer].answer}</p>
                  <p>{challenge.options[selectedAnswer].consequence}</p>
                  { 
                    Object.entries(effect).length > 0 &&
                    <div className="effects-taken">
                      <p>Você recebeu: </p>
                      { Object.entries(effect).map(([key, value], i) => 
                        {
                          const numericValue = value as number;

                          return <div key={i}>
                            <p>{numericValue > 0 ? `+${numericValue}` : numericValue}</p>
                            <img className="icon" src={IMAGES[key as keyof typeof IMAGES]} />
                          </div>;
                        })}
                    </div>
                  }
                </div>
                <button className="next-challenge-button" onClick={onNextChallenge}>Próximo</button>
              </>
            }
          </>
        }
      </div>
      <PlayerStatsElements playerStats={playerStats}/>
    </div>
  );
}



