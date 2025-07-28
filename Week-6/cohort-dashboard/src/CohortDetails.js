import React from 'react';
import styles from './CohortDetails.module.css';

const CohortDetails = ({ cohort }) => {
  const { name, startDate, status, coach, trainer } = cohort;

  return (
    <div className={styles.box}>
      <span
        className={`${styles.title} ${
          status.toLowerCase() === 'ongoing' ? styles.ongoing : styles.scheduled
        }`}
      >
        {name}
      </span>
      <dl>
        <dt>Started On</dt>
        <dd>{startDate}</dd>
        <dt>Current Status</dt>
        <dd>{status}</dd>
        <dt>Coach</dt>
        <dd>{coach}</dd>
        <dt>Trainer</dt>
        <dd>{trainer}</dd>
      </dl>
    </div>
  );
};

export default CohortDetails;
