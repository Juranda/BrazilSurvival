import IMAGES from "../../images.ts";
import { AnswerChallengeResult } from "../types/AnswerChallengeResult";


interface ChallengeConsequencesProps { 
  answerChallengeResult: AnswerChallengeResult,
  onNextChallenge: () => void
}

export default function ChallengeConsequences({ answerChallengeResult, onNextChallenge }: ChallengeConsequencesProps) {
  const { answer, consequence, effect } = answerChallengeResult;
  return (
    <>
      <div className="challenge-consequenses">
        <p className="answer">{answer}</p>
        <p>{consequence}</p>
        {Object.entries(effect).length > 0 &&
          Object.entries(effect).length > 0 &&
          <div className="effects-taken">
            {Object.entries(effect).map(([key, value], i) => {
              const numericValue = value as number;
              
              if (numericValue === undefined || numericValue === 0) return;
              
              const positivo = numericValue > 0;
              return <div key={i}>
                <p style={{color: positivo ? "green" : "red"}}>{positivo ? `+${numericValue}` : numericValue}</p>
                <img className="icon" src={IMAGES[key as keyof typeof IMAGES]} />
              </div>;
            })}
          </div>}
      </div>
      <button className="next-challenge-button" onClick={onNextChallenge}>Próximo</button>
    </>
  );
}
