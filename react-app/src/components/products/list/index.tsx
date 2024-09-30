import { useEffect, useState } from 'react';
import { httpService, BASE_URL } from '../../../api/http-service';
import { IProductItem } from '../../../interfaces/products';
import { Link } from 'react-router-dom';
import { Button } from 'antd';
import { PlusCircleFilled } from '@ant-design/icons';

const ProductListPage = () => {
    const [list, setList] = useState<IProductItem[]>([]);

    useEffect(() => {
        httpService.get<IProductItem[]>("/api/Products")
            .then(resp => {
                setList(resp.data);
            });
    }, []);

    return (
        <>
            <p className='text-center text-3xl font-bold mb-5'>Products</p>
            <Link to={"/products/create"}>
                <Button type="primary" shape="round" icon={<PlusCircleFilled />} style={{ marginTop: 10, marginBottom: 20 }} />
            </Link>

            <div className='grid md:grid-cols-3 lg:grid-cols-4 gap-10'>
                {list.map(i =>
                    <div key={i.id} className='border rounded-lg overflow-hidden shadow-lg'>
                        <img src={`${BASE_URL}/images/${i.images[0]}`} alt={i.name} className='w-full h-48 object-cover' />
                        <div className='p-4'>
                            <h3 className='text-xl font-semibold mb-2'>{i.name}</h3>
                            <p className='text-gray-700'>{i.price}</p>
                        </div>
                    </div>
                )}
            </div>
        </>
    );
}

export default ProductListPage;