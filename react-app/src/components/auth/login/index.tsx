import { LockOutlined, InboxOutlined } from '@ant-design/icons';
import { Button, Form, Input } from 'antd';
import { Link, useNavigate } from 'react-router-dom';
import { httpService } from '../../../api/http-service';
import { useDispatch } from 'react-redux';
import { loginSuccess } from '../authSlice';

const LoginPage = () => {

  const dispatch = useDispatch();
  const navigate = useNavigate();

  const onFinish = async (values: any) => {
    // console.log('Received values: ', values);
    const response = await httpService.post("api/auth/login", values);
    // console.log("resp: ", response.data.token);
    localStorage.setItem("accesstoken", response.data.token);
    dispatch(loginSuccess({
      // firstName: response.data.firstName,
      // lastName: response.data.lastName,
      firstName: "anme",
      lastName: response.data.lastName,
    }));
    navigate("/");
  };

  return (
    <div className="flex flex-col items-center justify-center">
      <p className='text-center text-3xl font-bold mb-10'>Login</p>
      <Form onFinish={onFinish} className="w-full max-w-sm">
        <Form.Item
          name="email"
          rules={[{ required: true, message: 'Please input your Email.' }]}
        >
          <Input prefix={<InboxOutlined />} placeholder="Email" />
        </Form.Item>

        <Form.Item
          name="password"
          rules={[{ required: true, message: 'Please input your Password.' }]}
        >
          <Input prefix={<LockOutlined />} type="password" placeholder="Password" />
        </Form.Item>

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
