import { useState, useEffect } from "react";
import { Button, Form, Modal, Input, Row, Upload, UploadFile } from "antd";
import { useNavigate, useParams } from "react-router-dom";
import { ICategoryEdit, IUploadedFile } from "../../../interfaces/categories/index";
import { httpService, BASE_URL } from '../../../api/http-service';
import { RcFile, UploadChangeParam } from "antd/es/upload";
import Loader from '../../common/loader/Loader';
import { PlusOutlined } from '@ant-design/icons';


const CategoryEditPage: React.FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();
    const [form] = Form.useForm<ICategoryEdit>();
    const [file, setFile] = useState<UploadFile | null>();
    const [loading, setLoading] = useState<boolean>(false);

    const [previewOpen, setPreviewOpen] = useState<boolean>(false);
    const [previewImage, setPreviewImage] = useState('');
    const [previewTitle, setPreviewTitle] = useState('');

    const onSubmit = async (values: ICategoryEdit) => {
        setLoading(true);
        try {
            const resp = await httpService.put<ICategoryEdit>("/api/categories",
                { ...values, id, Image: file },
                { headers: { "Content-Type": "multipart/form-data" } }
            );
            console.log("Update category", resp.data);
            navigate('/');
        } catch (error) {
            console.error("Error updating category", error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        setLoading(true);
        try {
            httpService.get<ICategoryEdit>(`/api/categories/${id}`)
                .then(resp => {
                    const { data } = resp;
                    form.setFieldsValue(data);
                    if (data.image != null) {
                        setFile({
                            uid: '-1',
                            name: data.name,
                            status: "done",
                            url: `${BASE_URL}/images/300_${data.image}`
                        });
                    }
                });
        } catch (error) {
            console.error("Error updating:", error);
        } finally {
            setLoading(false);
        }
    }, []);

    return (
        <>
            {loading ? (
                <Loader />
            ) : (
                <>
                    <p className="text-center text-3xl font-bold mb-7">Edit Category</p>
                    <Form form={form} onFinish={onSubmit} labelCol={{ span: 6 }} wrapperCol={{ span: 14 }}>
                        <Form.Item name="name" label="Name" hasFeedback
                            rules={[{ required: true, message: 'Please provide a valid category name.' }]}>
                            <Input placeholder='Type category name' />
                        </Form.Item>

                        <Form.Item name="description" label="Description" hasFeedback
                            rules={[{ required: true, message: 'Please enter some description.' }]}>
                            <Input.TextArea placeholder='Type some description' rows={4} />
                        </Form.Item>

                        <div className="flex items-center col-span-2 gap-x-2">
                            <Form.Item name="image" label="Фото" valuePropName="image"
                                getValueFromEvent={(e: UploadChangeParam) => {
                                    const image = e?.fileList[0] as IUploadedFile;
                                    return image?.originFileObj;
                                }}>
                                <Upload
                                    beforeUpload={() => false}
                                    accept="image/*"
                                    onPreview={(file: UploadFile) => {
                                        if (!file.url && !file.preview) {
                                            file.preview = URL.createObjectURL(file.originFileObj as RcFile);
                                        }

                                        setPreviewImage(file.url || (file.preview as string));
                                        setPreviewOpen(true);
                                        setPreviewTitle(file.name || file.url!.substring(file.url!.lastIndexOf('/') + 1));
                                    }}
                                    fileList={file ? [file] : []}
                                    onChange={(data) => {
                                        setFile(data.fileList[0]);
                                    }}
                                    listType="picture-card"
                                    maxCount={1}>
                                    <div>
                                        <PlusOutlined />
                                        <div style={{ marginTop: 8 }}>Upload</div>
                                    </div>
                                </Upload>
                            </Form.Item>
                        </div>

                        <Form.Item wrapperCol={{ span: 10, offset: 10 }}>
                            <Row>
                                <Button htmlType="reset" className='text-white bg-gradient-to-br from-red-400 to-purple-600 font-medium rounded-lg px-5 mr-2'>Reset</Button>
                                <Button htmlType="submit" className='text-white bg-gradient-to-br from-green-400 to-blue-600 font-medium rounded-lg px-5'>Update</Button>
                            </Row>
                        </Form.Item>
                    </Form>

                    <Modal open={previewOpen} title={previewTitle} footer={null} onCancel={() => setPreviewOpen(false)}>
                        <img alt="example" style={{ width: '100%' }} src={previewImage} />
                    </Modal>
                </>
            )}
        </>
    );
};

export default CategoryEditPage;
