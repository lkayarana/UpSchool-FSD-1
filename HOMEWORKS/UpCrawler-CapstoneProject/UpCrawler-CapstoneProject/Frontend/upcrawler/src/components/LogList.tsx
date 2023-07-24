import React from 'react';

interface Log {
    message: string;
    sentOn: string;
}

interface LogListProps {
    logs: Log[];
}

const LogList: React.FC<LogListProps> = ({ logs }) => {
    return (
        <div>
            {logs.map((log, index) => (
                <p key={index}>{log.message} | {log.sentOn}</p>
            ))}
        </div>
    );
};

export default LogList;
