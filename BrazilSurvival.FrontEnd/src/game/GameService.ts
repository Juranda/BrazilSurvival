import axios from "axios";
import PlayerStats from "./components/PlayerStats";
import { NextChallengeAnswer } from "./types/NextChallengeAnswer";
import { GameStartedResponse } from "./components/GameStartedResponse";
import PlayerScorePost from "./types/PlayerScorePost";

export default class GameService {

    private readonly gameEndpoint = axios.create({
        baseURL: import.meta.env.VITE_SERVER_URL + "/game",
        timeout: 50000,
        headers: {
            "Content-Type": "application/json"
        }
    });

    private readonly playerScoreEndpoint = axios.create({
        baseURL: import.meta.env.VITE_SERVER_URL + "/playersScores",
        timeout: 50000,
        headers: {
            "Content-Type": "application/json"
        }
    });


    public async startGame(gameStatus?: PlayerStats): Promise<GameStartedResponse> {
        const response = await this.gameEndpoint.post<GameStartedResponse>("/start", gameStatus);
        console.log(response)
        return response.data;
    }


    public async nextChallenge(playerStats: PlayerStats, challengeId: number, optionId: number, requestNewChallenges?: boolean): Promise<NextChallengeAnswer> {
        const response = await this.gameEndpoint.post<NextChallengeAnswer>("/next", {
            playerStats,
            challengeId,
            optionId,
            requestNewChallenges
        });

        return response.data;
    }


    public async savePlayerScore(name: string, score: number) {
        const response = await this.playerScoreEndpoint.post<PlayerScorePost>("", {
            name,
            score
        });

        return response.data;
    }
}



