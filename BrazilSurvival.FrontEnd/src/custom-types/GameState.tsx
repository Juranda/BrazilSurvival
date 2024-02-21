import PlayerStats from "../components/PlayerStats";
import Challenge from "../models/Challenge";

export interface GameState {
  gameIsLoading: boolean;
  gameIsOver: boolean;
  challenges: Challenge[];
  currentChallenge: Challenge;
  selectedAnswer: number;
  playerStats: PlayerStats;
  score: number;
}
