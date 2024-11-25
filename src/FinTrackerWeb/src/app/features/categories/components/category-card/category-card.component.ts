// src/app/features/categories/components/category-card/category-card.component.ts (Replace entire file)
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { TransactionCategoryDto, TransactionType } from '../../../../core/interfaces/category.interface';

@Component({
  selector: 'app-category-card',
  templateUrl: './category-card.component.html',
  styleUrls: ['./category-card.component.css']
})
export class CategoryCardComponent {
  @Input() category!: TransactionCategoryDto;
  @Output() delete = new EventEmitter<string>();

  TransactionType = TransactionType;

  constructor(private router: Router) { }

  onEdit(): void {
    this.router.navigate(['/categories/edit', this.category.id]);
  }

  onDelete(): void {
    this.delete.emit(this.category.id);
  }

  getTypeColor(): string {
    return this.category.type === TransactionType.Income ? 'green' : 'red';
  }
}