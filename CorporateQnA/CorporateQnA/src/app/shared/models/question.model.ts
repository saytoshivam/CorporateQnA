export class Question {
    public title: string;

    public description: string;

    public categoryId: number;

    public askedBy: number;

    constructor(args: {}) {
        this.askedBy = args['askedBy']
        this.categoryId = args['categoryId']
        this.description = args['description']
        this.title = args['title']
    }
}