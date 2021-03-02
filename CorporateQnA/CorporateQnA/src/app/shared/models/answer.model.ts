export class Answer {
    public questionId: number;

    public questionsAnswer: string;

    public answeredBy: number;

    constructor(args: {}) {
        this.answeredBy = args['answeredBy'];
        this.questionId = args['questionId'];
        this.questionsAnswer = args['questionsAnswer'];
    }
}