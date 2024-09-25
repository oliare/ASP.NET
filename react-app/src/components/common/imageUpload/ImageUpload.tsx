import React, { useState } from 'react';
import { Upload, Modal } from 'antd';
import type { UploadFile, RcFile } from 'antd/es/upload';

interface ImageUploadProps {
    file: UploadFile[];
    setFile: (fileList: UploadFile[]) => void;
}

const ImageUpload: React.FC<ImageUploadProps> = ({ file, setFile }) => {
    const [previewOpen, setPreviewOpen] = useState<boolean>(false);
    const [previewImage, setPreviewImage] = useState('');
    const [previewTitle, setPreviewTitle] = useState('');

    const handlePreview = (file: UploadFile) => {
        if (!file.url && !file.preview) {
            file.preview = URL.createObjectURL(file.originFileObj as RcFile);
        }

        setPreviewImage(file.url || (file.preview as string));
        setPreviewOpen(true);
        setPreviewTitle(file.name || file.url!.substring(file.url!.lastIndexOf('/') + 1));
    };

    const handleChange = ({ fileList }: { fileList: UploadFile[] }) => {
        setFile(fileList);
    };

    return (
        <>
            <Upload listType="picture-card" fileList={file} beforeUpload={() => false} accept="image/*"
                onPreview={handlePreview} onChange={handleChange} maxCount={1}>
                {file.length < 1 && '+ Upload'}
            </Upload>

            <Modal open={previewOpen} title={previewTitle} footer={null}
                onCancel={() => setPreviewOpen(false)}>
                <img style={{ width: '100%' }} src={previewImage} alt="Preview" />
            </Modal>
        </>
    );
};

export default ImageUpload;