export interface IAppUser{
  ID: number;
  UserName: string;
}

export class AppUser {
  constructor(public id: number, public userName: string) {}
}

export type AppUsers = AppUser[];
