import { assert, describe, it } from "vitest";
import GameService from "../components/gameService";

describe("GameService tests", () => {

    it("gameService should return new game", async () => {
        const gameService = new GameService();

        const gameStartResponse = await gameService.startGame();
        const playerStats = gameStartResponse.playerStats;
        const challenges = gameStartResponse.challenges;

        assert(playerStats.health === 10);
        assert(playerStats.money === 10);
        assert(playerStats.power === 10);

        assert(challenges.length > 0);
    });

    it("gameService should modify player stats", async () => {
        const gameService = new GameService();

        const gameStartResponse = await gameService.nextChallenge({ health: 20, money: 20, power: 20}, 0, 0);
        const { answer, consequence, isGameOver, newChallenges } = gameStartResponse;

        assert(answer.length > 0);
        assert(consequence.length > 0);

        assert(newChallenges?.length === 0);
        assert(isGameOver === false);
    });
})