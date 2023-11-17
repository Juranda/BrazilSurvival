import IMAGES from "../images";
import AnswerEffect from "../models/AnswerEffect";

interface ChallengeConsequencesProps { answer: string; consequence: string; effect: AnswerEffect; onNextChallenge: () => void; }
export default function ChallengeConsequences({ answer, consequence, effect, onNextChallenge }: ChallengeConsequencesProps) {
  return (
    <>
      <div className="challenge-consequenses">
        <p className="answer">{answer}</p>
        <p>{consequence}</p>
        {Object.entries(effect).length > 0 &&
          Object.entries(effect).length > 0 &&
          <div className="effects-taken">
            <p>Você recebeu: </p>
            {Object.entries(effect).map(([key, value], i) => {
              const numericValue = value as number;
              if (numericValue === undefined || numericValue === 0) return;

              return <div key={i}>
                <p>{numericValue > 0 ? `+${numericValue}` : numericValue}</p>
                <img className="icon" src={IMAGES[key as keyof typeof IMAGES]} />
              </div>;
            })}
          </div>}
      </div>
      <button className="next-challenge-button" onClick={onNextChallenge}>Próximo</button>
    </>
  );
}
