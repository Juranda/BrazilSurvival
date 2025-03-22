
import ChallengeQuestionary from "./ChallengeQuestionary";
import GameOver from "./GameOver";
import PlayerStatsElements from "../PlayerStats/PlayerStatsElements";
import ChallengeConsequences from "./ChallengeConsequences";
import useGame from "./useGame";
import './game.scss';

export default function Game() {
  const {
    error,
    hasSelectedAnswer,
    isOver,
    isLoading,
    score,
    currentChallenge,
    currentConsequences: consequences,
    playerStats,
    onAnswerSelected,
    onNextChallenge,
    start
  } = useGame();

  const renderState = () => {
    if (error) {
      return (
        <>
          <h2 className="title">Algo inesperado aconteceu</h2>
          <div>
            <p>Deseja tentar novamente?</p>
            <button onClick={start}>Sim</button>
          </div>
        </>
      );
    }

    if (isOver) {
      return (
        <>
          <h2 className="title">Game Over</h2>
          <GameOver
            score={score}
            restartGame={() => start()} />
        </>

      );
    }

    if (isLoading) {
      return (
        <>
          <h2 className="title">Carregando</h2>
          <div className="loading-challenges">
            <h1>Carregando desafios...</h1>
          </div>
        </>
      );
    }

    if (hasSelectedAnswer) {
      return (
        <>
          <h2 className="title">Brazil Survival</h2>
          <div className="game-body">
            <ChallengeConsequences
              answerChallengeResult={consequences}
              onNextChallenge={onNextChallenge} />

          </div>
        </>
      );
    }

    return (
      <>
        <h2 className="title">Brazil Survival</h2>
        <div className="game-body">
          <ChallengeQuestionary
            challenge={currentChallenge}
            onAnswerSelected={onAnswerSelected} />
        </div>
      </>
    )
  };

  return (
    <div className="game">
      {renderState()}
      <PlayerStatsElements playerStats={playerStats} />
    </div>
  );
}