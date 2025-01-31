import { useReducer, useEffect } from "react";
import { GameEventAction } from "../../custom-types/GameEventAction";
import { GameState } from "../../custom-types/GameState";
import Challenge from "../../models/Challenge";
import StaticDatabase from "../../static-database";
import ChallengeConsequences from "./ChallengeConsequences";
import ChallengeQuestionary from "./ChallengeQuestionary";
import GameOver from "./GameOver";
import PlayerStatsElements from "./PlayerStatsElements";

const ANSWER_NOT_SELECTED = -1;

const initialState: GameState = {
  gameIsLoading: true,
  gameIsOver: false,
  challenges: [],
  currentChallenge: {
    title: "",
    options: []
  },
  selectedAnswer: ANSWER_NOT_SELECTED,
  playerStats: {
    health: 8,
    money: 5,
    power: 3,
  },
  score: 0
};

export default function Game() {
    const [gameState, dispatchGameEvent] = useReducer(handleGameState, initialState);

    const {
      gameIsLoading, 
      gameIsOver, 
      currentChallenge, 
      selectedAnswer, 
      playerStats, 
      score 
    } = gameState;
  
    useEffect(() => {
      initializeGameState();
    }, []);
  
    /**
     * Inicializa o estado do jogo.
     * O estado poderia ser inicializado utilizando o `useReducer`, 
     * porém, como é preciso fazer uma chamada à API a função precisa ser asíncrona, um caso que o `useReducer` não cobre.
     */
    async function initializeGameState() {
      var newChallenges: Challenge[];
      var newChallenge: Challenge;
  
      newChallenges = await fetchRandomChallenges();
      newChallenge = newChallenges.pop() as Challenge;
  
      dispatchGameEvent({
        type: "on-game-started",
        payload: {
          challenges: newChallenges,
          currentChallenge: newChallenge,
          gameIsLoading: false,
        }
      });
    }
  
    async function fetchRandomChallenges() {
      var newChallenges: Challenge[];
      try {
        const response = await fetch(`http://localhost:${import.meta.env.SERVER_PORT}/challenges`);
        newChallenges = response.status === 200 ? await response.json() : await StaticDatabase.getRandomChallenges()
      } catch (error) {
        newChallenges = await StaticDatabase.getRandomChallenges();
      }
    
      return newChallenges;
    }
  
    /**
     * Processa um novo estado depois que um `dispatchGameEvent()` foi disparado.
     * @param state Estado atual do jogo
     * @param action Evento discipado
     * @returns Novo estado atualizado
     */
    function handleGameState(state: GameState, action: GameEventAction): GameState {
      const { health, money, power } = state.playerStats;
      switch (action.type) {
        case "on-game-started":
          return { 
            ...state, 
            currentChallenge: action.payload.currentChallenge as Challenge, 
            challenges: action.payload.challenges as Challenge[],
            gameIsLoading: false,
          }
        case "on-answer-selected":
          const newSelectedAnswer = action.payload.selectedAnswer;
  
          if(newSelectedAnswer === undefined) return { ...state };
          if(newSelectedAnswer < -1) return { ...state };
  
          const selectedOption = state.currentChallenge.options[newSelectedAnswer];
          
          return {
            ...state,
            selectedAnswer: newSelectedAnswer,
            playerStats: {
              health: health + (selectedOption.health || 0),
              money: money + (selectedOption.money || 0),
              power: power + (selectedOption.power || 0)
            }
          }
  
        case "on-next-challenge":
          const lastinhChallenges = [...state.challenges];
          const nextChallenge = lastinhChallenges.pop();
          const newScore = state.score + 1;
  
          if(nextChallenge === undefined) {
            getNewChallenges();
            return { 
              ...state, 
              selectedAnswer: ANSWER_NOT_SELECTED, 
              gameIsLoading: true, 
              score: newScore
            };
          }
  
          return {
            ...state,
            currentChallenge: nextChallenge,
            challenges: lastinhChallenges,
            selectedAnswer: ANSWER_NOT_SELECTED,
            score: newScore
          }
        
        case "on-game-over":
  
          return {
            ...state,
            gameIsOver: true,
            gameIsLoading: false,
          }
  
        case "on-game-restarted":
          initializeGameState();
  
          return {
            ...initialState
          }
  
        default: return { ...state };
      }
    }
  
    async function getNewChallenges() {
      const newChallenges = await fetchRandomChallenges();
      const newChallenge = newChallenges.pop() as Challenge;
      
      dispatchGameEvent({
        type: "on-game-started",
        payload: { 
          challenges: newChallenges,
          currentChallenge: newChallenge
        }
      });
    }
  
    function handleOnNextChallenge() {
      const { health, money, power } = playerStats;
  
      if(health <= 0 || money <= 0 || power <= 0) {
        dispatchGameEvent({
          type: "on-game-over",
          payload: {}
        });
        return;
      }
  
      dispatchGameEvent({
       type: "on-next-challenge",
       payload: {} 
      });
    }
  
    return (
      <div className="game">
        <h2 className="title">Brazil Survival</h2>
        {
          gameIsOver ? 
            <GameOver score={score} restartGame={() => dispatchGameEvent({
              type: "on-game-restarted",
              payload: {}
            })}/> :
            !gameIsLoading ?
            <div className="game-body">
              <>
                {
                  selectedAnswer === ANSWER_NOT_SELECTED ?
                  <ChallengeQuestionary 
                    challenge={currentChallenge} 
                    onAnswerSelected={(index: number) => dispatchGameEvent({
                      type: "on-answer-selected", 
                      payload: { selectedAnswer: index }
                    })}/> : 
                  <ChallengeConsequences 
                    selectedOption={currentChallenge.options[selectedAnswer]}
                    onNextChallenge={handleOnNextChallenge}/>
                }
                </>
              </div> : <div className="loading-challenges"><h1>Carregando desafios...</h1></div>
        }
        <PlayerStatsElements playerStats={playerStats}/>
      </div>
    );
}