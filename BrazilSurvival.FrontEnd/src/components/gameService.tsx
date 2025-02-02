import axios from "axios";
import Challenge from "../models/Challenge";
import PlayerStats from "./game/PlayerStats";

export default class GameService {

    private readonly httpClient = axios.create({
        baseURL: import.meta.env.VITE_SERVER_URL + "/game",
        timeout: 50000,
        headers: {
            "Content-Type": "application/json"
        }
    });

    public async startGame(gameStatus?: PlayerStats): Promise<GameStartedResponse> {
        const response = await this.httpClient.post<GameStartedResponse>("/start", gameStatus);

        return response.data;
    }


    public async nextChallenge(playerStats: PlayerStats, challengeId: number, optionId: number, requestNewChallenges?: boolean): Promise<NextChallengeAnswer> {
        const response = await this.httpClient.post<NextChallengeAnswer>("/next", {
            playerStats,
            challengeId,
            optionId,
            requestNewChallenges
        });

        return response.data;
    }
}


interface GameStartedResponse {
    playerStats: PlayerStats,
    challenges: Challenge[]
}

interface NextChallengeAnswer {
    answer: string,
    consequence: string,
    newPlayerStats: PlayerStats,
    isGameOver: boolean,
    newChallenges?: Challenge[]
}
