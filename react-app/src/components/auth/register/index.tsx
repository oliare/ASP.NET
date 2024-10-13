import { Button, Form, Input, Upload, Space } from 'antd';
import { PlusOutlined } from '@ant-design/icons';
import { Link, useNavigate } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import { registerSuccess } from '../authSlice';
import { useState } from 'react';
import { httpService } from '../../../api/http-service';

const RegisterPage = () => {

  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [image, setImage] = useState<ArrayBuffer | null | string | undefined>(null);
  const [form] = Form.useForm();

  const onFinish = async (values: any) => {
    try {
      console.log("User data before submission:", image);

      const checkUser = await httpService.post("api/auth/checkEmail", { email: values.email });
      if (checkUser.data.exists) {
        form.setFields([
          {
            name: 'email',
            errors: ['This email is already associated with another account.'],
          },
        ]);
        return;
      }

      const response = await httpService.post("api/auth/register", { ...values, image }
      );
      dispatch(registerSuccess(response.data.user));
      navigate("/auth/login");
    }
    catch (error) {
      console.error("Registration failed:", error);
    }
  };

  const handleImage = (info: any) => {
    if (info.fileList.length > 0) {
      const file = info.fileList[0].originFileObj;
      const reader = new FileReader();

      reader.onload = (event) => {
        const imageBase64 = event.target?.result;
        setImage(imageBase64);
      };
      reader.readAsDataURL(file);
    } else {
      setImage(null);
    }
  };

  return (
    <>
      <p className="text-center text-3xl font-bold mb-10">Registration</p>
      <Form form={form} onFinish={onFinish} labelCol={{ span: 8 }} wrapperCol={{ span: 10 }}>
        <Form.Item name="firstName" label="First Name" hasFeedback
          rules={[{ required: true, message: 'Please provide a valid First Name.' }]}>
          <Input placeholder="First Name" />
        </Form.Item>

        <Form.Item name="lastName" label="Last Name" hasFeedback
          rules={[{ required: true, message: 'Please provide a valid Last Name.' }]}>
          <Input placeholder="Last Name" />
        </Form.Item>

        <Form.Item name="email" label="Email" hasFeedback
          rules={[{ required: true, type: 'email', message: 'Please provide a valid email address.' }]}>
          <Input placeholder="Email" />
        </Form.Item>

        <Form.Item name="password" label="Password" hasFeedback
          rules={[
            { required: true, message: 'Please provide a valid password.' },
            { min: 6, message: 'Password must be at least 6 characters long.' }]}>
          <Input.Password placeholder="Password" />
        </Form.Item>

        <Form.Item name="image" label="Photo" valuePropName="file"
          rules={[{ required: true, message: "Please choose a photo." }]}>
          <Upload beforeUpload={() => false} accept="image/*" listType="picture-card"
            maxCount={1} onChange={handleImage}>
            <div>
              <PlusOutlined />
              <div style={{ marginTop: 8 }}>Upload</div>
            </div>
          </Upload>
        </Form.Item>

        <Form.Item wrapperCol={{ span: 10, offset: 10 }}>
          <Space>
            <Link to="/">
              <Button className="text-white bg-gradient-to-br from-green-200 to-blue-600 font-medium rounded-lg px-5">
                Cancel
              </Button>
            </Link>
            <Button htmlType="submit"
              className="text-white bg-gradient-to-br from-red-200 to-purple-600 font-medium rounded-lg px-5">
              Register
            </Button>
          </Space>

        </Form.Item>
      </Form>
    </>
  );
};

export default RegisterPage;
