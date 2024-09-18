import React, { useEffect, useState } from 'react';
import { httpService, BASE_URL } from '../../api/http-service';
import { ICategoryItem } from '../../interfaces/categories';
import { Link } from 'react-router-dom';

const HomePage: React.FC = () => {
  const [list, setList] = useState<ICategoryItem[]>([]);

  useEffect(() => {
    httpService.get<ICategoryItem[]>("/api/Categories")
      .then(resp => {
        console.log("Success:", resp);
        setList(resp.data);
      })
      .catch(error => {
        console.log("Error:", error);
      });
  }, []);

  return (
    <>
      <p className='text-center text-3xl font-bold mb-5'>Categories</p>
      <Link to={"/create"}>
        <button className="mb-4 text-white font-bold bg-gray-500 hover:bg-gray-600 py-1 px-4 rounded">
          Add
        </button>
      </Link>

      <div className='grid md:grid-cols-3 lg:grid-cols-4 gap-4'>
        {list.map(x => (
          <div key={x.id} className='border rounded-lg overflow-hidden shadow-lg'>
            <img
              src={`${BASE_URL}/images/${x.image}`}
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

export default HomePage;