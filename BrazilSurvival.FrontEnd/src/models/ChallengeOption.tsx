import AnswerEffect from "./AnswerEffect";

export default interface ChallengeOption {
  action: string;
  answer: string;
  consequence: string;
  effect: AnswerEffect;
}
