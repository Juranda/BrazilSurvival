import PlayerStats from "./PlayerStats";
import Challenge from "./Challenge";

export interface GameStartedResponse {
    playerStats: PlayerStats;
    challenges: Challenge[];
}
