import axios from "axios";
import PlayerStats from "./types/PlayerStats";
import { NextChallengeAnswer } from "./types/NextChallengeAnswer";
import { GameStartedResponse } from "./types/GameStartedResponse";
import PlayerScorePost from "./types/PlayerScorePost";
import { PlayerScore } from "../globalRank/PlayerScore";

export interface IGameService {
    startGame(gameStatus?: PlayerStats): Promise<GameStartedResponse>;
    nextChallenge(token: string, challengeId: number, optionId: number, requestNewChallenges?: boolean): Promise<NextChallengeAnswer>;
    savePlayerScore(name: string, score: number): void;
    getGlobalRank(page: number, pageSize: number, abortController: AbortController): Promise<PlayerScore[]>;
}

export default class GameService implements IGameService {
    private readonly gameEndpoint = axios.create({
        baseURL: import.meta.env.VITE_SERVER_URL + "/game",
        timeout: 50000,
        headers: {
            "Content-Type": "application/json"
        },
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

        return response.data;
    }

    public async nextChallenge(token: string, challengeId: number, optionId: number, requestNewChallenges?: boolean): Promise<NextChallengeAnswer> {
        const response = await this.gameEndpoint.post<NextChallengeAnswer>("/next", {
            token,
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

    public async getGlobalRank(page: number, pageSize: number = 10, abortController: AbortController = new AbortController()): Promise<PlayerScore[]> {
        const response = await this.playerScoreEndpoint.get<PlayerScore[]>("", {
            params: {
                page,
                pageSize
            },
            signal: abortController.signal
        });
        return response.data;
    }
}



