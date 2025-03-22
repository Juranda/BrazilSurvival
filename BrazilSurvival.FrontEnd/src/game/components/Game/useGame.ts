import { useReducer, useEffect, useContext, useState } from "react";
import Challenge from "../../types/Challenge";
import { GameEventAction } from "../../types/GameEventAction";
import { GameState } from "../../types/GameState";
import { GameServiceContext } from "../../../contexts";
import { NextChallengeAnswer } from "../../types/NextChallengeAnswer";
import PlayerStats from "../../types/PlayerStats";

const ANSWER_NOT_SELECTED = -1;

const initialState: GameState = {
    isOver: false,
    isLoading: false,
    challenges: [],
    currentChallenge: {
        id: 0,
        title: "",
        options: []
    },
    token: "",
    selectedAnswer: ANSWER_NOT_SELECTED,
    playerStats: {
        health: 0,
        money: 0,
        power: 0,
    },
    score: 0,
    nextChallengeResult: {
        isGameOver: false,
        newPlayerStats: {
            health: 0,
            money: 0,
            power: 0,
        },
        answer: "",
        consequence: "",
        effect: {
            health: 0,
            money: 0,
            power: 0,
        }
    }
};

/**
   * Processa um novo estado depois que um `dispatchGameEvent()` foi disparado.
   * @param state Estado atual do jogo
   * @param action Evento discipado
   * @returns Novo estado atualizado
   */
function handleGameState(state: GameState, action: GameEventAction): GameState {
    switch (action.type) {
        case "on-game-loading":
            return {
                ...state,
                isLoading: true
            }
        case "on-game-stop-loading":
            return {
                ...state,
                isLoading: false
            }
        case "on-game-started":
            return handleOnGameStartedDispatch(state, action.payload);
        case "on-answer-selected":
            return handleOnAnswerSelectedDispatch(state, action.payload);
        case "on-next-challenge":
            return handleOnNextChallengeDispatch(state);
        case "on-game-over":
            return {
                ...state,
                isOver: true,
            }
        default: return { ...state };
    }
}

function handleOnGameStartedDispatch(state: GameState, payload: Partial<GameState>): GameState {
    return {
        ...state,
        currentChallenge: payload.challenges?.pop() as Challenge,
        challenges: payload.challenges as Challenge[],
        isOver: false,
        selectedAnswer: ANSWER_NOT_SELECTED,
        playerStats: payload.playerStats as PlayerStats,
        token: payload.token as string,
    }
}

function handleOnAnswerSelectedDispatch(state: GameState, payload: Partial<GameState>): GameState {
    const newSelectedAnswer = payload.selectedAnswer;

    if (newSelectedAnswer === undefined) return { ...state };
    if (newSelectedAnswer < -1) return { ...state };

    return {
        ...state,
        selectedAnswer: newSelectedAnswer,
        nextChallengeResult: payload.nextChallengeResult as NextChallengeAnswer,
        playerStats: payload.nextChallengeResult?.newPlayerStats as PlayerStats
    }
}

function handleOnNextChallengeDispatch(state: GameState): GameState {
    var newChallenges = [...state.challenges];

    if (newChallenges.length === 0) {
        newChallenges = [...state.nextChallengeResult.newChallenges as Challenge[]];
    }

    const nextChallenge = newChallenges.pop() as Challenge;
    const newScore = state.score + 1;

    return {
        ...state,
        currentChallenge: nextChallenge,
        challenges: newChallenges,
        selectedAnswer: ANSWER_NOT_SELECTED,
        score: newScore
    }
}


const useGame = () => {
    const gameService = useContext(GameServiceContext);
    const [error, setError] = useState<Error | undefined>();
    const [gameState, dispatchGameEvent] = useReducer(handleGameState, initialState);

    useEffect(() => {
        initializeGameState();
    }, []);

    /**
     * Inicializa o estado do jogo.
     * O estado poderia ser inicializado utilizando o `useReducer`, 
     * porém, como é preciso fazer uma chamada à API a função precisa ser asíncrona, um caso que o `useReducer` não cobre.
     */
    async function initializeGameState() {
        if (gameState.isLoading) return;
        try {
            dispatchGameEvent({ type: "on-game-loading", payload: {} });
            const response = await gameService.startGame();

            const newGameState: Partial<GameState> = {
                challenges: response.challenges,
                playerStats: response.playerStats,
                token: response.token,
            };

            dispatchGameEvent({ type: "on-game-stop-loading", payload: {} });

            dispatchGameEvent({
                type: "on-game-started",
                payload: newGameState
            });
        } catch (err) {
            dispatchGameEvent({ type: "on-game-stop-loading", payload: {} });
            setError(err as Error);
        }
    }

    async function handleOnNextChallenge() {
        if (gameState.isLoading) return;

        dispatchGameEvent({ type: "on-game-loading", payload: {} });

        if (gameState.nextChallengeResult.isGameOver) {
            dispatchGameEvent({
                type: "on-game-over",
                payload: {}
            });
            return;
        }

        dispatchGameEvent({
            type: "on-next-challenge",
            payload: {
                challenges: gameState.nextChallengeResult.newChallenges as Challenge[]
            }
        });

        dispatchGameEvent({ type: "on-game-stop-loading", payload: {} });
    }

    async function handleOnAnswerSelected(selectedOption: number) {
        if (gameState.isLoading) return;

        try {
            dispatchGameEvent({ type: "on-game-loading", payload: {} });
            const nextChallengeAnswer = await gameService.nextChallenge(gameState.token, gameState.currentChallenge.id, selectedOption, gameState.challenges.length === 0);
            dispatchGameEvent({ type: "on-game-stop-loading", payload: {} });

            dispatchGameEvent({
                type: "on-answer-selected",
                payload: {
                    selectedAnswer: selectedOption,
                    nextChallengeResult: nextChallengeAnswer,
                    isLoading: false,
                }
            });

        } catch (err) {
            dispatchGameEvent({ type: "on-game-stop-loading", payload: {} });
            setError(err as Error);
        }
    }

    return {
        isLoading: gameState.isLoading,
        isOver: gameState.isOver,
        hasSelectedAnswer: gameState.selectedAnswer !== ANSWER_NOT_SELECTED,
        error,
        score: gameState.score,
        currentChallenge: gameState.currentChallenge,
        currentConsequences: gameState.nextChallengeResult,
        playerStats: gameState.playerStats,
        onNextChallenge: handleOnNextChallenge,
        onAnswerSelected: handleOnAnswerSelected,
        start: initializeGameState,
    };
}




export default useGame;