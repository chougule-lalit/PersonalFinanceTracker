// src/app/features/categories/pages/category-form/category-form.component.ts (Replace entire file)
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CategoryService } from '../../services/category.service';
import { TransactionType } from '../../../../core/interfaces/category.interface';

@Component({
  selector: 'app-category-form',
  templateUrl: './category-form.component.html',
  styleUrls: ['./category-form.component.css']
})
export class CategoryFormComponent implements OnInit {
  categoryForm: FormGroup;
  isEditMode = false;
  categoryId: string | null = null;
  TransactionType = TransactionType; // Add this line to make enum available in template
  transactionTypes = Object.keys(TransactionType)
    .filter(key => !isNaN(Number(key)))
    .map(key => Number(key));

  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService,
    private router: Router,
    private route: ActivatedRoute,
    private snackBar: MatSnackBar
  ) {
    this.categoryForm = this.fb.group({
      name: ['', Validators.required],
      description: [''],
      type: [null, Validators.required]
    });
  }

  ngOnInit(): void {
    this.categoryId = this.route.snapshot.paramMap.get('id');
    if (this.categoryId) {
      this.isEditMode = true;
      this.loadCategory(this.categoryId);
    }
  }

  loadCategory(id: string): void {
    this.categoryService.getCategory(id).subscribe({
      next: (category) => {
        this.categoryForm.patchValue({
          name: category.name,
          description: category.description,
          type: category.type
        });
      },
      error: () => {
        this.snackBar.open('Error loading category', 'Close', { duration: 3000 });
        this.router.navigate(['/categories']);
      }
    });
  }

  onSubmit(): void {
    if (this.categoryForm.valid) {
      if (this.isEditMode && this.categoryId) {
        this.categoryService.updateCategory(this.categoryId, {
          id: this.categoryId,
          ...this.categoryForm.value
        }).subscribe({
          next: () => {
            this.snackBar.open('Category updated successfully', 'Close', { duration: 3000 });
            this.router.navigate(['/categories']);
          },
          error: () => {
            this.snackBar.open('Error updating category', 'Close', { duration: 3000 });
          }
        });
      } else {
        this.categoryService.createCategory(this.categoryForm.value).subscribe({
          next: () => {
            this.snackBar.open('Category created successfully', 'Close', { duration: 3000 });
            this.router.navigate(['/categories']);
          },
          error: () => {
            this.snackBar.open('Error creating category', 'Close', { duration: 3000 });
          }
        });
      }
    }
  }
}