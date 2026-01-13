import React from "react";
import Cell from "../Cell/Cell";
import styles from "./Board.module.css";
import StartButton from "../ResetButton/ResetButton";
import { revealCell, createGame, toggleFlag } from "../../GameApi";
import { BoardState } from "../../models/Board";



interface CellState {
  isRevealed: boolean;
  isFlagged: boolean;
  hasMine: boolean;
  adjacentMines: number;
}

const Board = () => {
  const rows = 16;
  const columns = 16;

  const [board, setBoard] = React.useState<CellState[][]>(
    Array.from({ length: rows }, () =>
      Array.from({ length: columns }, () => ({
        isRevealed: false,
        isFlagged: false,
        hasMine: false,
        adjacentMines: 0,
      }))
    )
  );

  const [status, setStatus] = React.useState<"NotStarted" | "InProgress" | "Won" | "Lost">("NotStarted");

  const [started, setStarted] = React.useState(false);

  const newGame = async () => {
    try {
      const backendBoard = await createGame();
      setBoard(backendBoard.cells);
      setStarted(true);
    } catch (err) {
      console.error("Failed to start game", err);
    }
  };
  

  const handleClick = async (row: number, col: number) => {
    if (status === "Lost" || status === "Won") return;

    try {
      const updatedBoard = await revealCell(row, col);
      setBoard(updatedBoard.cells);
      setStatus(updatedBoard.status);
      if (updatedBoard.status === "Lost") {
        return;

      }
      if (updatedBoard.status === "Won") {
        return;

      }
    } catch (error) {
      console.error("Failed to reveal cell:", error);
    }
  };

  const handleRightClick = async (row: number, col: number) => {
    if (status === "Lost" || status === "Won") return;
    try {
      const updatedBoard = await toggleFlag(row, col);
      setBoard(updatedBoard.cells);
      setStatus(updatedBoard.status);
    } catch (error) {
      console.error("Failed to toggle flag:", error);
    }
  };

  const handleReset = async () => {
    const newBoard = await createGame();
    setBoard(newBoard.cells);
    setStatus("InProgress");
  };



  
  

  return (
    <div className={styles.boardWrapper}>
      <div className={styles.board}>
        
          <div className={styles.topPanel}>
            <StartButton onClick={handleReset} />
          </div>
        
        {board.map((row, rowIndex) =>
          row.map((cell, colIndex) => (
            <Cell
              key={`${rowIndex}-${colIndex}`}
              cell={cell}
              onClick={() => handleClick(rowIndex, colIndex)}
              onRightClick={() => {
                handleRightClick(rowIndex, colIndex);
              }}
            />
          ))
        )}
      </div>
    </div>
  );
};

export default Board;
