import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CategoryService } from '../../services/category.service';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-category-form',
  standalone: true,
  imports: [
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    ReactiveFormsModule 
  ],
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.css']
})
export class CategoryFormComponent implements OnInit {
  categoryForm: FormGroup;
  isEditMode = false;
  categoryId: number | null = null;

  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.categoryForm = this.fb.group({
      categoryID: [''], 
      name: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.isEditMode = true;
        this.categoryId = +id;
        this.loadCategory(this.categoryId);
      }
    });
  }

  loadCategory(id: number): void {
    this.categoryService.getCategory(id).subscribe((category: any) => {
      this.categoryForm.patchValue({
        categoryID: category.categoryID,
        name: category.name
      });
    });
  }

  onSubmit(): void {
    if (this.categoryForm.valid) {
      const categoryData = {
        categoryID: this.isEditMode ? this.categoryForm.get('categoryID')?.value : 0, 
        name: this.categoryForm.get('name')?.value
      };
      
      console.log(categoryData);
  
      if (this.isEditMode && this.categoryId) {
        this.categoryService.updateCategory(this.categoryId, categoryData).subscribe(() => {
          alert('Categoría actualizada exitosamente');
          this.router.navigate(['/categories']);
        });
      } else {
        this.categoryService.createCategory(categoryData).subscribe(() => {
          alert('Categoría creada exitosamente');
          this.router.navigate(['/categories']);
        });
      }
    }
  }
  

  onCancel(): void {
    this.router.navigate(['/categories']);
  }
}
