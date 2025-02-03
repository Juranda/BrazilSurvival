import ChallengeOption from "./ChallengeOption";

export default interface Challenge {
  id: number;
  title: string;
  options: ChallengeOption[];
}
