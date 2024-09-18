import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../services/category.service';
import { Category } from '../../models/category.model';
import { Router, RouterModule } from '@angular/router'; // Importa el servicio Router
import { MatTableModule } from '@angular/material/table';
import { MatButton } from '@angular/material/button';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [MatTableModule, RouterModule, MatButton], 
  templateUrl: './category-list.component.html'
})
export class CategoryListComponent implements OnInit {
  categories: Category[] = [];

  constructor(private categoryService: CategoryService, private router: Router) { }

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe(data => {
      this.categories = data;
    });
  }

  deleteCategory(id: number): void {
    if (confirm('Confirma?')) {
      this.categoryService.deleteCategory(id).subscribe(() => {
        this.categories = this.categories.filter(category => category.categoryID !== id);
      });
    }
  }

  editCategory(id: number): void {
    this.router.navigate(['/edit-category', id]); 
  }

  addCategory(): void {
    this.router.navigate(['/add-category']);
  }
  
}
