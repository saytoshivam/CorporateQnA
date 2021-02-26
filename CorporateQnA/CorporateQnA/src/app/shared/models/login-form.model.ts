export class LoginForm {
    public UserName: string;
    public Password: string;
    constructor(args) {
        this.UserName = args.username;
        this.Password = args.password;
    }
}