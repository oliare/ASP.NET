import { Button, Form, Input, Upload, Space } from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import { Link } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { registerSuccess } from '../authSlice';

const RegisterPage = () => {
  const dispatch = useDispatch();

  const onFinish = (values) => {
    console.log('Received values:', values);
  dispatch(registerSuccess(values)); 
  };

  return (
    <>
      <p className='text-center text-3xl font-bold mb-10'>Registration</p>
      <Form onFinish={onFinish} labelCol={{ span: 8 }} wrapperCol={{ span: 10 }}>
        <Form.Item name="firstName" label="First Name" hasFeedback
          rules={[{ required: true, message: 'Please provide a valid First Name.' }]}>
          <Input placeholder='First Name' />
        </Form.Item>

        <Form.Item name="lastName" label="Last Name" hasFeedback
          rules={[{ required: true, message: 'Please provide a valid Last Name.' }]}>
          <Input placeholder='Last Name' />
        </Form.Item>

        <Form.Item name="image" label="Photo" valuePropName="file"
          rules={[{ required: true, message: "Please choose a photo for the category." }]}>
          <Upload beforeUpload={() => false} accept="image/*" listType="picture-card" maxCount={1}>
            <div>
              <PlusOutlined />
              <div style={{ marginTop: 8 }}>Upload</div>
            </div>
          </Upload>
        </Form.Item>

        <Form.Item wrapperCol={{ span: 10, offset: 10 }}>
          <Space>
            <Link to={"/"}>
              <Button className='text-white bg-gradient-to-br from-green-200 to-blue-600 font-medium rounded-lg px-5'>Cancel</Button>
            </Link>
            <Button htmlType="submit" className='text-white bg-gradient-to-br from-red-200 to-purple-600 font-medium rounded-lg px-5'>Register</Button>
          </Space>
        </Form.Item>
      </Form>
    </>
  );
};

export default RegisterPage;
