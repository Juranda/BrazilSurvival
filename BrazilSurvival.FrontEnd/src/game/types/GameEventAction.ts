import { GameEventPayload } from "./GameEventPayload";

export interface GameEventAction {
  type: "on-game-started" | "on-answer-selected" | "on-next-challenge" | "on-game-over";
  payload: GameEventPayload;
}
