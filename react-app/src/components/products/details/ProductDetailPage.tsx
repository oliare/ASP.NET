import { useEffect, useState, useRef } from 'react';
import { httpService, BASE_URL } from '../../../api/http-service';
import { IProductItem } from '../../../interfaces/products';
import { useParams, Link } from 'react-router-dom';
import { Button, Carousel } from 'antd';
import { EditOutlined } from '@ant-design/icons';

const ProductDetailPage = () => {
    const { id } = useParams<{ id: string }>();
    const [product, setProduct] = useState<IProductItem | null>(null);

    const carouselRef = useRef<any>(null);

    useEffect(() => {
        const fetchProduct = async () => {
            try {
                const resp = await httpService.get<IProductItem>(`/api/products/${id}`);
                setProduct(resp.data);
            } catch (error) {
                console.log("Error fetching product: ", error);
            }
        };

        fetchProduct();
    }, [id]);

    const next = () => {
        carouselRef.current ? carouselRef.current.next() : '';
    };

    const prev = () => {
        carouselRef.current ? carouselRef.current.prev() : '';
    };

    return (
        <>
            <div className='container mx-auto p-4 mt-5 flex flex-col md:flex-row shadow-lg rounded-lg bg-white'>
                <div className='ml-5 md:w-1/2 relative'>
                    <Carousel ref={carouselRef} arrows={false} infinite>
                        {product?.images.map((image, index) => (
                            <div key={index}>
                                <img
                                    src={`${BASE_URL}/images/1200_${image}`}
                                    alt={product?.name}
                                    className='w-full h-[400px] object-cover'
                                />
                            </div>
                        ))}
                    </Carousel>

                    <div className="absolute inset-y-0 left-0 flex items-center" style={{ left: '-20px' }}>
                        <button onClick={prev}>
                            &#10094;
                        </button>
                    </div>
                    <div className="absolute inset-y-0 right-0 flex items-center" style={{ right: '-20px' }}>
                        <button onClick={next}>
                            &#10095;
                        </button>
                    </div>
                </div>

                <div className='md:w-1/2 ml-10 p-4 flex flex-col justify-between'>
                    <div>
                        <h1 className='text-2xl text-center font-semibold text-teal-700'>{product?.name}</h1>
                        <hr className='border-t-2 text-center mt-3 mb-8'></hr>
                        <p className='text-lg text-gray-800'>Price: <span className='italic text-teal-600'>${product?.price}</span></p>
                        <p className='text-lg text-gray-800'>Category: <span className='italic text-teal-600'>{product?.categoryName}</span></p>
                    </div>
                    <div className='flex justify-between mt-5'>
                        <Link to={`/products/edit/${product?.id}`}>
                            <Button icon={<EditOutlined />} className='text-white bg-gradient-to-br from-violet-400 to-red-400 font-medium rounded-lg px-5'>
                                Edit Product
                            </Button>
                        </Link>
                        <Link to='/products'>
                            <Button className='text-white bg-gradient-to-br from-green-400 to-blue-600 font-medium rounded-lg px-5'>Back to Products</Button>
                        </Link>
                    </div>
                </div>
            </div>
        </>
    );
};

export default ProductDetailPage;
