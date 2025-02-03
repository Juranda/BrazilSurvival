import PlayerStats from "../components/PlayerStats";
import Challenge from "./Challenge";
import { NextChallengeAnswer } from "./NextChallengeAnswer";

export interface GameState {
  gameIsLoading: boolean;
  gameIsOver: boolean;
  challenges: Challenge[];
  currentChallenge: Challenge;
  selectedAnswer: number;
  playerStats: PlayerStats;
  score: number;
  nextChallengeResult: NextChallengeAnswer
}