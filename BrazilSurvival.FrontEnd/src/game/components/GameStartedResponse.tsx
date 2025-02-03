import PlayerStats from "./PlayerStats";
import Challenge from "../types/Challenge";

export interface GameStartedResponse {
    playerStats: PlayerStats;
    challenges: Challenge[];
}
