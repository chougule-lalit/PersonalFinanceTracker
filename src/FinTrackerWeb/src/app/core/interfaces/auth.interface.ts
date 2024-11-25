export interface LoginRequest {
    email: string;
    password: string;
}

export interface RegisterRequest {
    email: string;
    password: string;
    firstName: string;
    lastName: string;
}

export interface LoginResponse {
    token: string;
    user: UserDto;
}

export interface UserDto {
    id: string;
    email: string;
    firstName: string;
    lastName: string;
    fullName?: string;
    isActive: boolean;
    createdAt: Date;
    roles?: string[];
}