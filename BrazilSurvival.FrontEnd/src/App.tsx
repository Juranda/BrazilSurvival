import { GameServiceContext } from "./contexts";
import GameService from "./game/GameService";
import { Auth } from "./auth/Auth";
import { BrowserRouter, Route, Routes } from "react-router";
import Game from "./game/components/Game/Game";

export default function App() {

  return (
    <BrowserRouter>
      <Routes>
        <Route path="auth" element={<Auth />} />
        <Route 
          path="game" 
          element={(
            <GameServiceContext.Provider value={new GameService()}>
              <Game />
            </GameServiceContext.Provider>
          )} />
      </Routes>
    </BrowserRouter>
  );
}


