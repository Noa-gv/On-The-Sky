export default class User {
  Id!: number;
  userName!: string;
  Password!: string;
  Role!: eRole;
}

export enum eRole {
  manager,
  passanger,
}
