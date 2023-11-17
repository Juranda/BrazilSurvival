import AnswerOption from "./AnswerOption";
import Challenge from "../models/Challenge";

interface ChallengeQuestionaryProps {
  challenge: Challenge,
  onAnswerSelected: (index: number) => void
}

export default function ChallengeQuestionary({ challenge, onAnswerSelected }: ChallengeQuestionaryProps) {
  return (
    <>
      <h4 className="challenge-title">{challenge.title}</h4>
      <div className="answer-options">
        {challenge.options.map((v, i) => <AnswerOption key={i} onAnswerSelected={() => onAnswerSelected(i)} text={`${i + 1}. ${v.action}`} />)}
      </div>
    </>
  );
}
