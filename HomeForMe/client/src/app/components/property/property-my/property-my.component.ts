import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { PropertyService } from 'src/app/services/property.service';

@Component({
  selector: 'app-property-my',
  templateUrl: './property-my.component.html',
  styleUrls: ['./property-my.component.css']
})
export class PropertyMyComponent implements OnInit {
  properties: any = [];

  constructor(
    private propertyService: PropertyService,
    private toastrService: ToastrService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.propertyService.getMine()
      .subscribe((response: any) => {
        console.log(response)
      }, (error: any) => {
        if (error.status == 401) {
          this.toastrService.error('Cannot access this page!')
          this.router.navigateByUrl('/');
        }
        console.log(error);
      });
  }

}
