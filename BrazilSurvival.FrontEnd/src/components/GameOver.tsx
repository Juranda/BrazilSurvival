import { useRef, useState } from "react";

interface GameOverProps {
  score: number,
  restartGame: () => void
}

type GameOverState = "over" | "registerning" | "registered" | "fail";

export default function GameOver({ score, restartGame }: GameOverProps) {
  const [gameOverState, setGameOverState] = useState<GameOverState>("over");
  const playerNameRefs = Array.from({ length: 6 }, () => useRef<HTMLInputElement>(null));
  const [playerName, setPlayerName] = useState(["", "", ""]);

  async function onPlayerNameSubimited() {
    if (playerName.length < 3) {
      return;
    }

    setGameOverState("registerning");
    try {
      await fetch(`http://localhost:${import.meta.env.SERVER_PORT}/players`, {
        headers: {
          "Content-Type": "application/json",
        },
        method: "POST",
        body: JSON.stringify({
          name: playerName.reduce((prev, curr) => prev += curr),
          score: score
        })
      });

      setGameOverState("registered");
    } catch (error) {
      setGameOverState("fail");
    }
  }

  function renderState() {
    switch (gameOverState) {
      case "over":
        return (
          <div className="over-initial">
            <h2 className="game-over-title">Você perdeu!</h2>
            <p className="game-over-desc">{score} desafios vencidos</p>
            <p className="game-over-desc">Registre o seu progresso e continue tentando</p>
            <div className="inputs">
              {
                Array.from({ length: 6 }, (_, index) => (
                  <input key={index} ref={playerNameRefs[index]} onChange={e => handleInputChange(e, index)} type="text" maxLength={1} />
                ))
              }
            </div>
            <button onClick={onPlayerNameSubimited}>Enviar</button>
          </div>
        );
      case "registerning":
        return (<div>Registrando...</div>);
      case "registered":
        setTimeout(restartGame, 1000);
        return (<div><p>Seu progresso foi registrado!</p></div>);
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

  function handleInputChange(e: React.ChangeEvent<HTMLInputElement>, index: number) {
    const value = e.target.value;

    if ((value === "" || value === " ") && index > 0) {
      const input = playerNameRefs[index - 1].current;
      input?.focus();
      updatePlayerName("", index);

    } else if (isValidInput(value) && index < playerNameRefs.length - 1 && playerNameRefs[index + 1].current) {
      const input = playerNameRefs[index + 1].current;
      input?.focus();
      input?.select();
      updatePlayerName(value, index);
    }
  }

  function updatePlayerName(newLetter: string, position: number) {
    setPlayerName(prev => {
      const newName = [...prev];
      newName[position] = newLetter;
      return newName;
    });
  }

  function isValidInput(key: string) {
    return !((key.charCodeAt(0) >= 65 && key.charCodeAt(0) <= 90));
  }

  return (
    <div className="game-over">
      {renderState()}
    </div>
  );
}


