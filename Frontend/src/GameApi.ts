import { BoardState } from "./models/Board";
import { CellState } from "./models/Cell";



export const revealCell = async (row: number, column: number) => {
  const response = await fetch("http://localhost:5266/api/games/reveal", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    credentials: "include",
    body: JSON.stringify({ row, column }),
  });

  if (!response.ok) throw new Error("Failed to reveal cell");


  return response.json(); // backend returns the updated board
}
  
export async function createGame() {
  const response = await fetch("http://localhost:5266/api/games/create", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    credentials: "include",
  });

  if (!response.ok) {
    throw new Error("Failed to start game");
  }

  return response.json(); // returns Board
}

export const toggleFlag = async (row: number, column: number) => {
  const response = await fetch("http://localhost:5266/api/games/flag", {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    credentials: "include",
    body: JSON.stringify({ row, column }),
  });

  if (!response.ok) throw new Error("Failed to toggle flag");

  return response.json(); // backend returns the updated board
}

