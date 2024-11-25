export enum TransactionType {
    Income = 0,
    Expense = 1
}

export interface TransactionCategoryDto {
    id: string;
    name: string;
    description: string;
    type: TransactionType;
    isDefault: boolean;
    createdAt: Date;
}

export interface CreateTransactionCategoryDto {
    name: string;
    description: string;
    type: TransactionType;
}

export interface UpdateTransactionCategoryDto {
    id: string;
    name: string;
    description: string;
}