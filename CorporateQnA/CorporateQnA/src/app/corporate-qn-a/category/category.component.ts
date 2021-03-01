import { Component, OnInit } from '@angular/core';
import { CategoryDetails } from '../../shared/models';
import { CategoryService } from '../../shared/services';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styles: []
})
export class CategoryComponent implements OnInit {

  categoryDetailsList: CategoryDetails[];

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.categoryService.getAllCategories().subscribe(res => {
      this.categoryDetailsList = <CategoryDetails[]>res;
      console.log(this.categoryDetailsList);
    });
  }
}