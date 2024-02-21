
import IMAGES from "../images";
import PlayerStats from "./PlayerStats";

interface PlayerStatsElementsProps {
  playerStats: PlayerStats,
}

export default function PlayerStatsElements({ playerStats }: PlayerStatsElementsProps) {
  return <div className="player-stats-element">
    <div>
      <img className="icon" src={IMAGES.health} />
      <p>{playerStats.health}</p>
    </div>
    <div>
      <img className="icon" src={IMAGES.money} />
      <p>{playerStats.money.toLocaleString('pt-BR', { style: 'currency', currency: 'BRL' })}</p>
    </div>
    <div>
      <img className="icon" src={IMAGES.power} />
      <p>{playerStats.power}</p>
    </div>
  </div>;
}
