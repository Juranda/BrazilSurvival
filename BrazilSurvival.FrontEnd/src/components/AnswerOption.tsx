interface AnswerOptionProps {
  onAnswerSelected: () => void;
  text: string;
}

export default function AnswerOption({ onAnswerSelected, text }: AnswerOptionProps) {
  return <button className="answer-option" onClick={onAnswerSelected}>{text}</button>;
}
