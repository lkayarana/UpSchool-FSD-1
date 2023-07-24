import React, { useEffect, useState } from 'react';
import { HubConnectionBuilder } from '@microsoft/signalr';
import LogList from '../components/LogList';
import './CrawlerLogScreen.css';

interface Log {
    message: string;
    sentOn: string;
}

const CrawlerLogScreen: React.FC = () => {
    const [logs, setLogs] = useState<Log[]>([]);

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl('https://localhost:7243/Hubs/SeleniumLogHub')
            .build();

        connection.on('NewSeleniumLogAdded', (log: Log) => {
            setLogs((prevLogs) => [...prevLogs, log]);
        });

        connection
            .start()
            .then(() => console.log('SignalR connected.'))
            .catch((error) => console.error('SignalR connection error: ', error));

        return () => {
            connection.stop();
        };
    }, []);

    return (
        <div className="crawler-terminal space shadow">
            <div className="crawler-top">
                <div className="crawler-btns">
                    <span className="circle crawler-red"></span>
                    <span className="circle crawler-yellow"></span>
                    <span className="circle crawler-green"></span>
                </div>
                <div className="crawler-title">
                    <b>Crawler Live Logs Screen</b>
                </div>
            </div>
            <pre className="crawler-body">
        <LogList logs={logs} />
      </pre>
        </div>
    );
};

export default CrawlerLogScreen;