import { PlayerScore } from "../globalRank/PlayerScore";
import { IGameService } from "./GameService";
import { GameStartedResponse } from "./types/GameStartedResponse";
import { NextChallengeAnswer } from "./types/NextChallengeAnswer";
import PlayerStats from "./types/PlayerStats";

export default class DummyGameService implements IGameService {
    startGame(gameStatus?: PlayerStats): Promise<GameStartedResponse> {
        return Promise.resolve({
            token: "",
            challenges: [
                {
                    id: 1,
                    title: "Dummy question 1",
                    options: [
                        { id: 1, action: "Option 1" },
                        { id: 2, action: "Option 2" }
                    ]
                },
                {
                    id: 2,
                    title: "Dummy question 2",
                    options: [
                        { id: 3, action: "Option 3" },
                        { id: 4, action: "Option 4" }
                    ]
                }
            ],
            playerStats: {
                health: 10,
                money: 10,
                power: 10
            }
        });
    }
    nextChallenge(token: string, challengeId: number, optionId: number, requestNewChallenges?: boolean): Promise<NextChallengeAnswer> {
        return Promise.resolve({
            answer: "dummy doo",
            consequence: "dummy consequence",
            newPlayerStats: {
                health: 10,
                money: 10,
                power: 10
            },
            isGameOver: false,
            newChallenges: requestNewChallenges ? [
                {
                    id: 1,
                    title: "Dummy question 1",
                    options: [
                        { id: 1, action: "Option 1" },
                        { id: 2, action: "Option 2" }
                    ]
                },
                {
                    id: 2,
                    title: "Dummy question 2",
                    options: [
                        { id: 3, action: "Option 3" },
                        { id: 4, action: "Option 4" }
                    ]
                }
            ] : [],
            effect: {
                health: 0,
                money: 0,
                power: 0
            }
        });
    }
    savePlayerScore(name: string, score: number): void {
        console.log(`Player score saved: ${name} - ${score}`);
    }
    getGlobalRank(page: number, pageSize: number, abortController: AbortController): Promise<PlayerScore[]> {
        return Promise.resolve([
            {
                id: 0,
                position: 2,
                name: "Dummy 1",
                score: 10,
                timestamp: new Date()
            },
            {
                id: 1,
                position: 1,
                name: "Dummy 2",
                score: 20,
                timestamp: new Date()
            }
        ]);
    }

}
