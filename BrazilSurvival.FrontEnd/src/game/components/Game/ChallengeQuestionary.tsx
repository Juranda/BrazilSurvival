import AnswerOption from "./AnswerOption";
import Challenge from "../../types/Challenge";

interface ChallengeQuestionaryProps {
  challenge: Challenge,
  onAnswerSelected: (index: number) => void
}

export default function ChallengeQuestionary({ challenge, onAnswerSelected }: ChallengeQuestionaryProps) {
  return (
    <>
      <h4 className="challenge-title">{challenge.title}</h4>
      <div className="answer-options">
        {
          challenge.options.map((challengeOption, i) => 
            <AnswerOption 
              key={challengeOption.id} 
              onAnswerSelected={() => onAnswerSelected(challengeOption.id)} 
              text={`${i + 1}. ${challengeOption.action}`} 
            />)
        }
      </div>
    </>
  );
}
