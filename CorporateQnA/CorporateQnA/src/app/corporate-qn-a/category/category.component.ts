import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { redoIcon, plusIcon, searchIcon } from '../../shared/constants';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Category, CategoryDetails } from '../../shared/models';
import { CategoryService } from '../../shared/services';

@Component({
    selector: 'app-category',
    templateUrl: './category.component.html',
    styles: []
})
export class CategoryComponent implements OnInit {

    //ICONS
    faRedo = redoIcon;
    faPlus = plusIcon;
    faSearch = searchIcon;

    searchForm: FormGroup;
    newCategoryForm: FormGroup

    modalRef: BsModalRef;
    user: any
    allCategories: CategoryDetails[] = []
    showCategories: CategoryDetails[] = []
    show

    constructor(private modalService: BsModalService, private categoryService: CategoryService) {
        this.searchForm = new FormGroup({
            search: new FormControl(),
            show: new FormControl(0)
        })

        this.newCategoryForm = new FormGroup({
            categoryName: new FormControl("", [Validators.required]),
            categoryDescription: new FormControl("", [Validators.required])
        })


        this.user = null
    }

    ngOnInit() {
        this.categoryService.getAllCategories().subscribe(categories => {
            this.allCategories = <CategoryDetails[]>categories;
            this.showCategories = <CategoryDetails[]>categories;
        })

        this.searchForm.valueChanges.subscribe(value => {

            this.showCategories = this.allCategories.filter((e, i, a) => {
                let selected = true;
                if ((value['search'] ?? "").length != 0) {
                    selected = new RegExp(value['search'].replace(".", "\\."), "ig").exec(e.name) != null
                }
                return selected;
            })
        })
    }

    openModal(template: TemplateRef<any>) {
        this.modalRef = this.modalService.show(template, { class: "custom-modal" });
    }

    createCategory() {

        let name = this.newCategoryForm.get('categoryName').value;
        let description = this.newCategoryForm.get('categoryDescription').value;

        let category: Category = new Category({ name, description })

        this.categoryService.postCategory(category).subscribe(value => {

            let newCategoryDetail: CategoryDetails = new CategoryDetails({ name, description, tagsThisWeek: 0, tagsThisMonth: 0, totalTags: 0, id: value })

            this.allCategories.push(newCategoryDetail);
            this.showCategories.push(newCategoryDetail)

            this.newCategoryForm.reset();
            this.modalRef.hide();

        })
    }

    resetForm() {
        this.searchForm.reset();
        this.searchForm.get('show').patchValue(0)
    }
}