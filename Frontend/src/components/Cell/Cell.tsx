
import styles from "./Cell.module.css";
import { CellState } from "../../models/Cell";


interface Data {
  isRevealed: boolean;
  isFlagged: boolean;
  hasMine: boolean;
  adjacentMines: number;
}

interface Props {
  cell: Data;
  onClick: () => void;
  onRightClick: () => void;
}

const Cell = ({ cell, onClick, onRightClick }: Props) => {
  let content = "";

  if (cell.isRevealed) {
    if (cell.hasMine) {
      content = "💣";
    } else if (cell.adjacentMines && cell.adjacentMines > 0) {
      content = cell.adjacentMines.toString();
    }
  } else if (cell.isFlagged) {
    content = "🚩";
  }
  return (
    <button
      className={`${styles.cell} ${cell.isRevealed ? styles.revealed : ""}`}
      onClick={onClick}
      onContextMenu={(event) => {
        event.preventDefault();
        onRightClick();
      }}
    >{content}</button>
  );
};

export default Cell;
