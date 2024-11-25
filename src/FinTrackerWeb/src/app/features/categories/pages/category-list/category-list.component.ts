import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CategoryService } from '../../services/category.service';
import { TransactionCategoryDto, TransactionType } from '../../../../core/interfaces/category.interface';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css']
})
export class CategoryListComponent implements OnInit {
  categories$: Observable<TransactionCategoryDto[]>;
  selectedType: TransactionType = TransactionType.Income;  // Default to Income
  TransactionType = TransactionType;  // Make enum available in template

  constructor(
    private categoryService: CategoryService,
    private router: Router,
    private snackBar: MatSnackBar
  ) {
    this.categories$ = this.categoryService.getCategoriesByType(this.selectedType);
  }

  ngOnInit(): void { }

  onTypeChange(type: TransactionType): void {
    this.selectedType = type;
    this.categories$ = this.categoryService.getCategoriesByType(type);
  }

  onAddCategory(): void {
    this.router.navigate(['/categories/new']);
  }

  onDeleteCategory(id: string): void {
    if (confirm('Are you sure you want to delete this category?')) {
      this.categoryService.deleteCategory(id).subscribe({
        next: () => {
          this.snackBar.open('Category deleted successfully', 'Close', { duration: 3000 });
          this.categories$ = this.categoryService.getCategoriesByType(this.selectedType);
        },
        error: () => {
          this.snackBar.open('Error deleting category', 'Close', { duration: 3000 });
        }
      });
    }
  }
}