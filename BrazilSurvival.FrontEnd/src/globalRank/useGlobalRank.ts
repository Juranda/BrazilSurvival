import { useContext, useEffect, useState } from "react";
import { GameServiceContext } from "../contexts";
import { PlayerScore } from "./PlayerScore";

const abortController = new AbortController();

export default function usePlayerScores(pageSize: number) {
    const gameService = useContext(GameServiceContext);

    const [isFetching, setIsFetching] = useState(false);
    const [playerScores, setPlayerScores] = useState<PlayerScore[]>([]);

    const [currentPage, setPage] = useState(1);

    function nextPage() {
        setPage(currentPage + 1);
        fetchPlayerScores(currentPage + 1, pageSize);
    }

    function previousPage() {
        if (currentPage > 1) {
            setPage(currentPage - 1);
            fetchPlayerScores(currentPage - 1, pageSize);
        }
    }

    async function fetchPlayerScores(page: number, pageSize: number) {
        if (isFetching) {
            abortController.abort();
            console.log("Request aborted");
        }

        setIsFetching(true);
        const scores = await gameService.getGlobalRank(page, pageSize, abortController);
        await new Promise(resolve => setTimeout(resolve, 1000));
        setIsFetching(false);

        setPlayerScores(scores);
    }

    useEffect(() => {
        fetchPlayerScores(currentPage, pageSize);
    }, []);

    return { playerScores, nextPage, previousPage, isLoading: isFetching, currentPage };
}