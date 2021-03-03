export class Category {

    public id: number;

    public name: string;

    public description: string;

    constructor(args: {}) {
        this.name = args['name'];
        this.description = args['description'];
        this.id = args['id'];
    }
}