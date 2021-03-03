export class CategoryDetails {
    public id: number;

    public name: string;

    public description: string;

    public totalTags: number;

    public tagsThisWeek: number;

    public tagsThisMonth: number;

    constructor(args: {}) {
        this.id = args['id']
        this.name = args['name']
        this.description = args['description']
        this.tagsThisWeek = args['tagsThisWeek']
        this.tagsThisMonth = args['tagsThisMonth']
        this.totalTags = args['totalTags']
    }
}