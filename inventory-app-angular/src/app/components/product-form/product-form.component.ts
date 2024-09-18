import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { CategoryService } from '../../services/category.service';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatTableModule } from '@angular/material/table';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-form',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    MatTableModule,
    ReactiveFormsModule
  ],
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.css']
})
export class ProductFormComponent implements OnInit {
  productForm: FormGroup;
  isEditMode = false;
  productId: number | null = null;
  categories: any[] = [];
  selectedCategories: number[] = [];

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private router: Router,
    private cd: ChangeDetectorRef
  ) {
    this.productForm = this.fb.group({
      productID: [''],
      name: ['', Validators.required],
      description: [''],
      image: ['']
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.isEditMode = true;
        this.productId = +id;
        this.loadProduct(this.productId);
      }
    });
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getCategories().subscribe((categories: any) => {
      this.categories = categories;
      this.cd.detectChanges();
    });
  }

  loadProduct(id: number): void {
    this.productService.getProductById(id).subscribe((product: any) => {
      this.productForm.patchValue({
        productID: product.productID,
        name: product.name,
        description: product.description,
        image: product.image,
        categories: product.categories
      });

      if (product.categories) {
        this.selectedCategories = product.categories.map((cat: any) => cat.categoryID);
      }
    });
  }

  toggleCategorySelection(categoryID: number, isChecked: boolean): void {
    if (isChecked) {
      this.selectedCategories.push(categoryID);
    } else {
      this.selectedCategories = this.selectedCategories.filter(id => id !== categoryID);
    }
  }

  isCategorySelected(categoryID: number): boolean {
    return this.selectedCategories.includes(categoryID);
  }

  onSubmit(): void {
    if (this.productForm.valid) {
      const productData = {
        ...this.productForm.value,
        categories: this.selectedCategories.map(id => {
          return { categoryID: id };
        })
      };

      if (!this.isEditMode) {
        delete productData.productID;
      }

      if (this.isEditMode && this.productId) {
        this.productService.updateProduct(this.productId, productData).subscribe(() => {
          alert('Producto actualizado exitosamente');
          this.router.navigate(['/products']);
        });
      } else {
        this.productService.addProduct(productData).subscribe(() => {
          alert('Producto creado exitosamente');
          this.router.navigate(['/products']);
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/products']);
  }
}