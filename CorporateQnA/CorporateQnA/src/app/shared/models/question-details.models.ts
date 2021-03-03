export class QuestionDetails {
    public id: number;

    public title: string;

    public description: string;

    public categoryId: number;

    public askedOn: Date;

    public askedBy: number;

    public userImage: string;

    public viewCount: number;

    public upVoteCount: number;

    public answerCount: number;

    public isResolved: boolean;

    constructor(args: {}) {
        this.id = args['id']
        // this.userName = args['userName']
        this.title = args['title']
        this.description = args['description']
        this.askedBy = args['askedBy']
        this.askedOn = args['askedOn']
        this.categoryId = args['categoryId']
        this.upVoteCount = args['upthis.upVoteCount']
        this.viewCount = args['viewCount']
        this.isResolved = args['isResolved']
        this.answerCount = args['answerCount']
    }
}