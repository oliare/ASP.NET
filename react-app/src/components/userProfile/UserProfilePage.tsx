import { useDispatch, useSelector } from 'react-redux';
import { useNavigate } from 'react-router-dom';
import { loginSuccess, logoutSuccess } from '../auth/authSlice';
import { useEffect } from 'react';
import { RootState } from '../store';
import { BASE_URL } from '../../api/http-service';
import { jwtDecode } from 'jwt-decode';
import { IUser } from '../../interfaces/auth';
import defaultIcon from '../../assets/default_avatar.jpg';
import { Form, Input, Row, Col, Image, Button } from 'antd';
import { LogoutOutlined, MailOutlined, TeamOutlined, UserOutlined } from '@ant-design/icons';

const UserProfilePage = () => {
    const navigate = useNavigate();
    const dispatch = useDispatch();
    const user = useSelector((state: RootState) => state.auth.user);

    const handleLogout = (value: string) => {
        if (value !== "Sign out") return;
        localStorage.removeItem('accesstoken');
        dispatch(logoutSuccess());
        navigate('/auth/login');
    };

    useEffect(() => {
        const accesstoken = localStorage.getItem("accesstoken");
        if (accesstoken) {
            const user = jwtDecode<IUser>(accesstoken);
            if (!user) {
                navigate('/auth/login');
            } else {
                dispatch(loginSuccess({
                    firstName: user.firstName || '',
                    lastName: user.lastName || '',
                    email: user.email,
                    image: user.image ? `${BASE_URL}/images/300_${user.image}` : defaultIcon,
                    roles: user.roles,
                    token: accesstoken
                }));
            }
        } else {
            navigate('/auth/login');
        }
    }, [dispatch, navigate]);


    return (
        <div className="flex justify-center items-center mt-10">
            <Form
                className="w-[600px] rounded-[15px] shadow-xl p-6"
                layout="vertical">

                <div className="flex justify-center mt-4 mb-10 drop-shadow-2xl">
                    <Image src={user?.image} alt={`${user?.firstName}'s profile`}
                        width={150} height={150} className="rounded-full" />
                </div>

                <Row gutter={16}>
                    <Col xs={24} md={12}>
                        <Form.Item label="First Name" name="firstName">
                            <Input disabled placeholder={user?.firstName} suffix={<UserOutlined />} />
                        </Form.Item>
                    </Col>
                    <Col xs={24} md={12}>
                        <Form.Item label="Last Name" name="lastName">
                            <Input disabled placeholder={user?.lastName} suffix={<UserOutlined />} />
                        </Form.Item>
                    </Col>
                    <Col xs={24} md={12}>
                        <Form.Item label="Email" name="email">
                            <Input disabled placeholder={user?.email} suffix={<MailOutlined />} />
                        </Form.Item>
                    </Col>
                    <Col xs={24} md={12}>
                        <Form.Item label="Role" name="role">
                            <Input disabled placeholder={
                                Array.isArray(user?.roles)
                                    ? user.roles.join(", ")
                                    : user?.roles || "No roles assigned"
                            } suffix={<TeamOutlined />} />
                        </Form.Item>
                    </Col>
                </Row>
                <div className="flex justify-center mt-4">
                    <Button className='text-white bg-gradient-to-br from-gray-400 to-indigo-400 font-medium rounded-lg px-5'
                        onClick={() => handleLogout("Sign out")}>
                        Log out <LogoutOutlined />
                    </Button>
                </div>
            </Form>
        </div>
    );
};

export default UserProfilePage;