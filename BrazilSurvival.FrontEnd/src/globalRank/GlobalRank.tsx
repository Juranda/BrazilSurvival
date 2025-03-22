import usePlayerScores from "./useGlobalRank";
import "./GlobalRank.scss";

export default function GlobalRank() {
    const { playerScores, previousPage, nextPage, isLoading, currentPage } = usePlayerScores(10);

    return <div id="global-rank">
        <>
            <table>
                <thead>
                    <tr>
                        <th className="global-rank-table-title" colSpan={5}>Rank Global</th>
                    </tr>
                    <tr>
                        <th>Posição</th>
                        <th>Nome</th>
                        <th>Pontuação</th>
                        {/* <th>Data</th> */}
                    </tr>
                </thead>
                <tbody>
                    {
                        isLoading ?
                            <tr>
                                <td colSpan={200}>Carregando</td>
                            </tr> :
                            playerScores.sort((a, b) => a.position - b.position).map((playerScore) => <tr key={playerScore.id}>
                                <td>{playerScore.position}</td>
                                <td>{playerScore.name}</td>
                                <td>{playerScore.score}</td>
                                {/* <td>{playerScore.timestamp ? playerScore.timestamp.toLocaleString() : ""}</td> */}
                            </tr>)}
                </tbody>
            </table>
            {
                !isLoading &&
                <div className="pagination-container">
                    <p>Página {currentPage}</p>
                    <button style={currentPage == 1 ? { display: "none" } : undefined} onClick={() => previousPage()}>{"<"}</button>
                    <button onClick={() => nextPage()}>{">"}</button>
                </div>
            }
        </>
    </div>;
}