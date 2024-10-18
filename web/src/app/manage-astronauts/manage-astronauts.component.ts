import { Component } from '@angular/core';

import { PersonService } from '../services/person/person.service';
import { Person } from '../models/person/person';
import { GetPeopleResponse } from '../models/person/getPeopleResponse';
import { NotificationService } from '../services/notification/notification.service';

@Component({
  selector: 'app-manage-astronauts',
  templateUrl: './manage-astronauts.component.html',
  styleUrl: './manage-astronauts.component.scss'
})
export class ManageAstronautsComponent {
  people: Person[] = [];
  loading = true;
  errorMessage = '';

  constructor(
    private personService: PersonService,
    private notificationService: NotificationService) { }

  ngOnInit(): void {
    this.loadAstronauts();
  }

  loadAstronauts(): void {
    this.personService.getPeople().subscribe({
      next: (data) => {
        this.people = data.people;
      },
      error: (error) => {
        this.notificationService.notify('An error occurred while fetching astronauts.');
      }
    });
  }
}
