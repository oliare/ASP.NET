import React, { useEffect, useState } from 'react';
import httpService from '../../api/http-service';
import { ICategoryItem } from '../../interfaces/categories';

const Categories: React.FC = () => {
  const [list, setList] = useState<ICategoryItem[]>([]);

  useEffect(() => {
    httpService.get<ICategoryItem[]>("/api/Categories")
      .then(resp => {
        const { data } = resp;
        console.log("Success:", data);
        setList(data);
      })
      .catch(error => {
        console.log("Error:", error);
      });
  }, []);

  return (
    <>
      <h1 className='text-center text-3xl font-bold mb-4 '>Categories</h1>
      <div className='grid md:grid-cols-3 lg:grid-cols-4 gap-4'>
        {list.map(x => (
          <div key={x.id} className='border rounded-lg overflow-hidden shadow-lg'>
            <img
              src={`${httpService}/images/${x.image}`}
              alt={x.name}
              className='w-full h-48 object-cover'
            />
            <div className='p-4'>
              <h3 className='text-xl font-semibold mb-2'>{x.name}</h3>
              <p className='text-gray-700'>{x.description}</p>
            </div>
          </div>
        ))}
      </div>
    </>
  );
};

export default Categories;
