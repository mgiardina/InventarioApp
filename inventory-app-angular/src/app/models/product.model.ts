import { Category } from "./category.model";

export interface Product {
    productID: number;
    name: string;
    description: string;
    image: string;
    categories: Category[];
  }