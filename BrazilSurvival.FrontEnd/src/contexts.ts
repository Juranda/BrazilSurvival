import { createContext } from "react";
import GameService from "./game/GameService";



const GameServiceContext = createContext<GameService>(new GameService());



export {
    GameServiceContext
}