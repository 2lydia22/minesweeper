import React from "react";
import styles from "./ResetButton.module.css";

interface Props {
  onClick: () => void;
}

const ResetButton = ({ onClick }: Props) => {
  return <button className ={styles.startButton} onClick={onClick}>Reset</button>;
};

export default ResetButton;
