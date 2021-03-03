export class Category {
    public name: string;

    public description: string;

    constructor(args: {}) {
        this.name = args['name'];
        this.description = args['description'];
    }
}