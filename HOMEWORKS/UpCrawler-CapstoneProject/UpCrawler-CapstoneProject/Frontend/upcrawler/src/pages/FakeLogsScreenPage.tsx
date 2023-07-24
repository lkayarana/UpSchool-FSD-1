import React from 'react';
import CrawlerLogScreen from './CrawlerLogScreen';
import './FakeLogsScreen.css'; // CSS dosyası

const FakeLogScreen: React.FC = () => {
    return (
        <div className="fake-log-screen">
            <div className="crawler-log">
                <CrawlerLogScreen />
            </div>

        </div>
    );
};

export default FakeLogScreen;