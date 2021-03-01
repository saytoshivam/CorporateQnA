export class UserProfile {
    public userId: number;
    public userName: string;
    public userImage: string;


    constructor(userId, userName, userImage) {
        this.userId = userId;
        this.userName = userName;
        this.userImage = userImage;
    }
}