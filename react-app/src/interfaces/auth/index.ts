export interface IUserLogin {
    email: string;
    password: string;
}

export interface IUserRegister {
    firstName: string;
    lastName: string;
    image: string;
    email: string;
    password: string;
}

export interface IUser {
    firstName: string;
    lastName: string;
    email: string;
    image: string;  
    token: string; 
    roles: string[];
}

export interface IAuthResponse {
    user: IUser;
    token: string; 
  }