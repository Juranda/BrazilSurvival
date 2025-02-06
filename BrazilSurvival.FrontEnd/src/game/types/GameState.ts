import PlayerStats from "./PlayerStats";
import Challenge from "./Challenge";
import { NextChallengeAnswer } from "./NextChallengeAnswer";

export interface GameState {
  isOver: boolean;
  isLoading: boolean;
  challenges: Challenge[];
  currentChallenge: Challenge;
  selectedAnswer: number;
  playerStats: PlayerStats;
  score: number;
  nextChallengeResult: NextChallengeAnswer
}