
import IMAGES from "../images";
import PlayerStats from "./PlayerStats";

interface PlayerStatsElementsProps {
  playerStats: PlayerStats,
}

export default function PlayerStatsElements({ playerStats }: PlayerStatsElementsProps) {
  return <div className="player-stats-element">
    <div>
      <img className="icon" src={IMAGES.vida} />
      <p>{playerStats.vida}</p>
    </div>
    <div>
      <img className="icon" src={IMAGES.dinheiro} />
      <p>{playerStats.dinheiro.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</p>
    </div>
    <div>
      <img className="icon" src={IMAGES.poder} />
      <p>{playerStats.poder}</p>
    </div>
  </div>;
}
