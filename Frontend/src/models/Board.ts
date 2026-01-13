import { CellState } from './Cell';

export interface BoardState {
    rows: number;
    columns: number;
    cells: CellState[][];
    totalCells: number;
    totalMines: number;
    minesPlaced: boolean;
    cellsRevealed: number;
    status: 'NotStarted' | 'InProgress' | 'Won' | 'Lost';
  }
  