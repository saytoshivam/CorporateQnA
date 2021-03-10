export class Filter {

        public id: number;

        public columnName: string;

        public type: filterType;

        public isNullCheck: boolean;
}

enum filterType {
  Integer,
  String,
  Date,
  Boolean
}
