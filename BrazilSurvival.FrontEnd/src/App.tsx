import { GameServiceContext } from "./contexts";
import { Auth } from "./auth/Auth";
import { BrowserRouter, Route, Routes } from "react-router";
import Game from "./game/components/Game/Game";
import Challenges from "./challenges/Challenges";
import GlobalRank from "./globalRank/GlobalRank";
import DummyGameService from "./game/DummyGameService";


const gameService = new DummyGameService();


export default function App() {
  return (
    <GameServiceContext.Provider value={gameService}>
      <BrowserRouter>
        <Routes>
          <Route path="challenges" element={<Challenges />} />
          <Route path="auth" element={<Auth />} />
          <Route path="rank" element={<GlobalRank />} />
          <Route
            index
            path="game"
            element={(
              <Game />
            )} />
        </Routes>
      </BrowserRouter>
    </GameServiceContext.Provider>
  );
}


