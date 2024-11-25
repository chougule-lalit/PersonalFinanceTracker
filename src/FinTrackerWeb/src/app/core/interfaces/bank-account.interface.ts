export enum AccountType {
    Savings = 0,
    Checking = 1,
    CreditCard = 2
}

export interface BankAccountDto {
    id: string;
    accountName: string;
    bankName: string;
    accountType: AccountType;
    currentBalance: number;
    accountNumber: string;
    createdAt: Date;
    lastModifiedAt?: Date;
}

export interface CreateBankAccountDto {
    accountName: string;
    bankName: string;
    accountType: AccountType;
    initialBalance: number;
    accountNumber: string;
}

export interface UpdateBankAccountDto {
    id: string;
    accountName: string;
    bankName: string;
    accountNumber: string;
}