import React from 'react';
import { Spin } from 'antd';

const Loader: React.FC = () => {
    return (
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh' }}>
            <Spin />
        </div>
    );
};

export default Loader;