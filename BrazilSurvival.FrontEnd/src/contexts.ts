import { createContext } from "react";
import GameService, { IGameService } from "./game/GameService";

const GameServiceContext = createContext<IGameService>(new GameService());

export {
    GameServiceContext
}