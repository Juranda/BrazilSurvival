export default interface ChallengeOption {
  action: string;
  answer: string;
  consequence: string;
  health?: number;
  money?: number;
  power?: number;
}
