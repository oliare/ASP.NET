import { LockOutlined, MailOutlined } from '@ant-design/icons';
import { Button, Form, Input, Alert } from 'antd';
import { Link, useNavigate } from 'react-router-dom';
import { httpService } from '../../../api/http-service';
import { useDispatch } from 'react-redux';
import { loginSuccess } from '../authSlice';
import { IAuthResponse, IUserLogin } from '../../../interfaces/auth';
import { useState } from 'react';

const LoginPage = () => {

  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [error, setError] = useState<string | null>(null);

  const onFinish = async (values: IUserLogin) => {
    try {
      const response = await httpService.post<IAuthResponse>("api/auth/login", values);
      localStorage.setItem("accesstoken", response.data.token);

      dispatch(loginSuccess(response.data.user));
      navigate("/userProfile");

    } catch (e) {
      setError('Invalid credentials.');
    }
  };

  return (
    <div className="flex flex-col items-center justify-center">
      <p className='text-center text-3xl font-bold mb-10'>Login</p>
      <Form onFinish={onFinish} className="w-full max-w-sm">
        <Form.Item
          name="email"
          rules={[{ required: true, message: 'Please input your Email.' }]}>
          <Input prefix={<MailOutlined />} placeholder=" Email" />
        </Form.Item>

        <Form.Item
          name="password"
          rules={[{ required: true, message: 'Please input your Password.' }]}>
          <Input.Password prefix={<LockOutlined />} type="password" placeholder=" Password" />
        </Form.Item>

        {error && (
          <Alert message={error} type="error" className='mb-5' />
        )}

        <Form.Item>
          <Button block htmlType="submit"
            className='text-white bg-gradient-to-br from-red-400 to-purple-500 font-medium rounded-lg mb-5'>
            Log in
          </Button>

          <div className="text-center">
            Don't have an account?
            <Link to={"/auth/register"} className='text-indigo-600'> Register</Link> now!
          </div>
        </Form.Item>

      </Form>
    </div>

  );
};

export default LoginPage;
