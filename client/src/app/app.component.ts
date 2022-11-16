import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { AppUsers, IAppUser } from './models/AppUser.model';
import { HttpService } from './services/http.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  users!: AppUsers;

  constructor(private httpService: HttpService) {}

  ngOnInit() {
    this.httpService.getUsers().subscribe({
      next: (response: AppUsers) => {
        this.users = response;
      },
      error: (error: HttpErrorResponse) => {
        console.error(error);
      }
    });
  }
}
