export interface IProductItem {
    id: number;
    name: string,
    price: string,
    images: string[],
    categoryName: string,
    categoryId: number,
}

export interface IProductCreate {
    name: string,
    price: number,
    categoryId: number,
    images: File[]|null,
}

export interface IProductEdit{
    name: string,
    price: number,
    categoryId: number,
    prevImages: File[],
    newImages: File[],
}