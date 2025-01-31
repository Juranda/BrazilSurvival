import IMAGES from "../../images";
import AnswerEffect from "../../models/AnswerEffect";
import ChallengeOption from "../../models/ChallengeOption";

interface ChallengeConsequencesProps { 
  selectedOption: ChallengeOption,
  onNextChallenge: () => void
}

export default function ChallengeConsequences({ selectedOption, onNextChallenge }: ChallengeConsequencesProps) {
  const { answer, consequence } = selectedOption;
  const effect = {
    health: selectedOption.health,
    money: selectedOption.money,
    power: selectedOption.power
  } as AnswerEffect
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
