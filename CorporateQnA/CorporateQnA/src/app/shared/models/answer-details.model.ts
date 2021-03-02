export class AnswerDetails {
    public id: number;

    public fullName: string;

    public answer: string;

    public userImage: string;

    public answeredOn: Date;

    public totalLikes: number;

    public totalDislikes: number;

    public isBestSolution: boolean;

    constructor(args: {}) {
        this.id = args['id']
        this.totalLikes = args['totalLikes']
        this.totalDislikes = args['totalDislikes']
        this.answer = args['answer']
        this.answeredOn = args['answeredOn']
        this.fullName = args['fullname']
        this.isBestSolution = args['isBestSolution']
    }
}