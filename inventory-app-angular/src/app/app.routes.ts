import { Routes } from '@angular/router';
import { ProductListComponent } from './components/product-list/product-list.component';
import { ProductFormComponent } from './components/product-form/product-form.component';
import { CategoryListComponent } from './components/category-list/category-list.component';
import { CategoryFormComponent } from './components/category-form/category-form.component';

export const routes: Routes = [
  { path: '', redirectTo: '/products', pathMatch: 'full' },
  { path: 'products', component: ProductListComponent }, 
  { path: 'add-product', component: ProductFormComponent }, 
  { path: 'edit-product/:id', component: ProductFormComponent }, 
  { path: 'categories', component: CategoryListComponent }, 
  { path: 'add-category', component: CategoryFormComponent }, 
  { path: 'edit-category/:id', component: CategoryFormComponent } 
];
