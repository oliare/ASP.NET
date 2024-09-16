import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import httpService from "../../../api/http-service";

const CategoryCreate: React.FC = () => {
    const [values, setValues] = useState({
        name: '',
        description: '',
        file: null as File | null
    });
    const [previewUrl, setPreviewUrl] = useState<string | null>(null);

    const navigate = useNavigate();

    const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
        const { name, value } = e.target;
        setValues(prev => ({ ...prev, [name]: value }));
    };

    const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const file = e.target.files?.[0] || null;
        setValues(prev => ({ ...prev, file }));
        if (file) {
            setPreviewUrl(URL.createObjectURL(file));
        } else {
            setPreviewUrl(null);
        }
    };

    const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    const { name, description, file } = values;

    if (!name || !description || !file) {
        alert("Please fill all fields");
        return;
    }

    try {
        const formData = new FormData();
        formData.append("name", name);
        formData.append("description", description);
        formData.append("image", file);

        await httpService.post("/api/categories", formData, {
            headers: { "Content-Type": "multipart/form-data" }
        });
        console.log("Category created");
        navigate("/");
    } catch {
        alert('Smth went wrong...');
    }
};

    const handleReset = () => {
        setValues({ name: '', description: '', file: null });
        setPreviewUrl(null);
    };

    return (
        <>
            <p className="text-center text-3xl font-bold mb-5">Create Category</p>
            <div className="max-w-lg mx-auto mt-10 p-6 rounded-lg shadow-md">
                <form onSubmit={handleSubmit} className="space-y-4">
                    <div>
                        <label htmlFor="name" className="font-medium">Name</label>
                        <input id="name" name="name" type="text" value={values.name} onChange={handleInputChange}
                            className="mt-1 block w-full border border-gray-300 rounded-md shadow-sm py-2 px-3" />
                    </div>
                    <div>
                        <label htmlFor="description" className="font-medium">Description</label>
                        <textarea id="description" name="description" value={values.description} onChange={handleInputChange}
                            className="mt-1 block w-full border border-gray-300 rounded-md shadow-sm py-2 px-3" />
                    </div>
                    <div>
                        <label htmlFor="file" className="block font-medium">Image</label>
                        <input id="file" type="file" accept="image/*" onChange={handleFileChange} className="mt-2" />
                        {previewUrl && (
                            <img src={previewUrl} alt="Preview" className="mt-4 w-32 h-32 object-cover" />
                        )}
                    </div>

                    <div className="flex justify-center space-x-4">
                        <button type="button" onClick={handleReset}
                            className="inline-flex justify-center py-2 px-4 border text-sm font-medium rounded-md text-white bg-gray-500 hover:bg-gray-700">
                            Reset
                        </button>
                        <button type="submit" className="inline-flex justify-center py-2 px-4 border text-sm font-medium rounded-md text-white bg-gray-500 hover:bg-gray-700">
                            Create
                        </button>
                    </div>
                </form>
            </div>
        </>
    );
}

export default CategoryCreate;
