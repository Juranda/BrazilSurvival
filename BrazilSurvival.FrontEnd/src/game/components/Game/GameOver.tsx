import { FormEvent, useContext, useEffect, useRef, useState } from "react";
import { GameServiceContext } from "../../../contexts";

interface GameOverProps {
  score: number,
  restartGame: () => void
}

type GameOverState = "over" | "registerning" | "registered" | "fail";

export default function GameOver({ score, restartGame }: GameOverProps) {
  const [gameOverState, setGameOverState] = useState<GameOverState>("over");
  const [playerName, setPlayerName] = useState("");
  const [invalidPlayerName, setInvalidPlayerName] = useState("");
  const playerNameRef = useRef<HTMLInputElement>(null)
  const gameService = useContext(GameServiceContext);

  useEffect(() => {
    playerNameRef.current?.select();
  }, [])

  async function onPlayerNameSubimited(event: FormEvent) {
    event.preventDefault();
    if (playerName.length < 6) {
      setInvalidPlayerName("\"" + playerName.toUpperCase() + "\" é inválido preencha as 6 letras")
      return;
    }

    setGameOverState("registerning");
    try {
      await gameService.savePlayerScore(playerName, score);

      setGameOverState("registered");
    } catch (error) {
      setGameOverState("fail");
    }
  }

  function renderState() {
    switch (gameOverState) {
      case "over":
        return (
          <form onSubmit={onPlayerNameSubimited} className="over-initial">
            <h2 className="game-over-title">Você perdeu!</h2>
            <p className="game-over-desc">{score} desafios vencidos</p>
            <p className="game-over-desc">Registre o seu progresso e continue tentando</p>
            <div className="game-over-input">
              <input ref={playerNameRef} onChange={e => handleInputChange(e)} type="text" style={{ color: "transparent" }} />
              <div className="inputs">
                {
                  playerName.padEnd(6, " ").split('').map((c, i) => <p key={i} className={playerName.length === i ? "selected" : ""} >{c}</p>)
                }
              </div>
            </div>
            {
              invalidPlayerName && <p>{invalidPlayerName}</p>
            }
            <button onClick={onPlayerNameSubimited}>Enviar</button>
          </form>
        );
      case "registerning":
        return (<div>Registrando...</div>);
      case "registered":
        return (
          <div className="restart-game">
            <h4 className="restart-game-title">Seu progresso foi registrado!</h4>
            <p>Deseja jogar novamente?</p>
            <div className="restart-game-options">
              <button onClick={restartGame}>Sim</button>
              <button>Não</button>
            </div>
          </div>
        );
      case "fail":
        return (
          <div className="over-error">
            <h3>Não foi possível registrar</h3>
            <p>Deseja tentar novamente, ou começar outra rodada?</p>
            <div className="buttons">
              <button onClick={onPlayerNameSubimited}>Tentar registar novamente</button>
              <button onClick={restartGame}>Começar outra rodada</button>
            </div>
          </div>);
    }
  }

  function handleInputChange(e: React.ChangeEvent<HTMLInputElement>) {
    const value = e.target.value;

    if (isValidInput(value)) {
      setPlayerName(value);
    } else {
      e.target.value = playerName;
    }
  }

  function isValidInput(value: string) {
    return /^[a-zA-Z0-9]{0,6}$/.test(value);
  }

  return (
    <div className="game-over">
      {renderState()}
    </div>
  );
}


