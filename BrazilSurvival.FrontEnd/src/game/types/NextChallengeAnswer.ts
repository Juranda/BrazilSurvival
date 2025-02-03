import PlayerStats from "../components/PlayerStats";
import AnswerEffect from "./AnswerEffect";
import Challenge from "./Challenge";

export interface NextChallengeAnswer {
    answer: string;
    consequence: string;
    newPlayerStats: PlayerStats;
    isGameOver: boolean;
    newChallenges?: Challenge[];
    effect: AnswerEffect;
}
