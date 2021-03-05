import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { Category } from '../models';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  constructor(private http: HttpClient) { }
  readonly baseUrl = 'api/category';


  postCategory(category: Category): Observable<any> {
    return this.http.post(`${this.baseUrl}/add`, category);
  }

  getAllCategories(): Observable<any> {
    return this.http.get(this.baseUrl + '/details');
  }
}
