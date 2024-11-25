export interface TransactionDto {
    id: string;
    transactionDate: Date;
    amount: number;
    description: string;
    transactionType: number;
    categoryName: string;
    accountName: string;
    createdAt: Date;
}

export interface CreateTransactionDto {
    transactionDate: Date;
    amount: number;
    description: string;
    transactionType: number;
    categoryId: string;
    bankAccountId?: string;
    creditCardId?: string;
}

export interface UpdateTransactionDto {
    id: string;
    transactionDate: Date;
    amount: number;
    description: string;
    transactionType: number;
    categoryId: string;
}