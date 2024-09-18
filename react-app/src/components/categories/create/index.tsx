import React, { useState } from 'react';
import { httpService } from "../../../api/http-service";
import { Form, Input, Button, Upload, Space } from 'antd';
import type { UploadProps, UploadFile } from 'antd/es/upload';
import { useNavigate } from 'react-router-dom';

const CategoryCreate: React.FC = () => {
    const [fileList, setFileList] = useState<UploadFile[]>([]); 
    const navigate = useNavigate();

    const onFinish = async (values: any) => {
        const formData = new FormData();
        formData.append('name', values.name);
        formData.append('description', values.description);
        if (fileList.length > 0) 
            formData.append('image', fileList[0].originFileObj as File);
        try {
            const response = await httpService.post("/api/categories", formData);
            console.log("Category creation successful", response);
            navigate("/");
        } catch (error) {
            console.log("Something went wrong during the category creation", error);
        }
    };

    const onChange: UploadProps['onChange'] = ({ fileList: newFileList }) => {
        setFileList(newFileList); 
    };

    const onPreview = async (file: UploadFile) => {
        const src = file.url || URL.createObjectURL(file.originFileObj as File);
        const imgWindow = window.open(src);
        imgWindow?.document.write(` 
            <div style="display: flex; justify-content: center; align-items: center; height: 100%;">
                <img src="${src}" style="max-width: 100%;"/>
            </div>`
        );
    };

    return (
        <>
            <p className="text-center text-3xl font-bold mb-7">Create Category</p>
            <Form onFinish={onFinish} labelCol={{ span: 6 }} wrapperCol={{ span: 14 }}>
                <Form.Item name="name" label="Name" hasFeedback
                    rules={[{ required: true, message: 'Please provide a valid category name.' }]} >
                    <Input placeholder='Type category name' />
                </Form.Item>

                <Form.Item name="description" label="Description" hasFeedback
                    rules={[{ required: true, message: 'Please enter some description.' }]} >
                    <Input.TextArea placeholder='Type some description' rows={4} />
                </Form.Item>

                <Form.Item name="image" label="Image" hasFeedback
                    rules={[{ required: true, message: "Please choose a photo for the category." }]} >
                    <Upload listType="picture-card" fileList={fileList} maxCount={1}
                        onChange={onChange} onPreview={onPreview}>
                        {fileList.length <= 1 && '+ Upload'}
                    </Upload>
                </Form.Item>

                <Form.Item wrapperCol={{ span: 10, offset: 10 }}>
                    <Space>
                        <Button htmlType="reset" className='text-white bg-gradient-to-br from-red-400 to-purple-600 font-medium rounded-lg px-5'>Reset</Button>
                        <Button htmlType="submit" className='text-white bg-gradient-to-br from-green-400 to-blue-600 font-medium rounded-lg px-5'>Create</Button>
                    </Space>
                </Form.Item>
            </Form>
        </>
    );
}

export default CategoryCreate;