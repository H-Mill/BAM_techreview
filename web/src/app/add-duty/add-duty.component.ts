import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Person } from '../models/person/person';
import { NotificationService } from '../services/notification/notification.service';
import { DutyService } from '../services/duty/duty.service';
import { PersonService } from '../services/person/person.service';
import { AddDutyRequest } from '../models/astronautDuty/addDutyRequest';

@Component({
  selector: 'app-add-duty',
  templateUrl: './add-duty.component.html',
  styleUrl: './add-duty.component.scss'
})
export class AddDutyComponent {
  loadedPerson: Person = <Person>{}
  dutyName: string = ''
  dutyStartDate: Date = new Date()

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private personService: PersonService,
    private dutyService: DutyService,
    private notificationService: NotificationService) { }

  ngOnInit() {
    this.dutyStartDate.setDate(this.dutyStartDate.getDate() + 1)
    this.loadExistingAstronaut();
  }

  loadExistingAstronaut(): void {
    this.activatedRoute.params.subscribe(params => {
      const personId = params['personId'];
      if (personId === undefined) return;

      this.personService.getPersonById(personId).subscribe({
        next: (data) => {
          this.loadedPerson = data.person;
        },
        error: (response) => {
          this.notificationService.notifyErrorResponse(response.error);
        }
      });
    });
  }

  submit(): void {
    const newDuty: AddDutyRequest = {
      personId: this.loadedPerson.id,
      dutyName: this.dutyName,
      dutyStartDate: this.dutyStartDate
    };
    this.dutyService.addDuty(newDuty).subscribe({
      next: data => {
        this.router.navigate([`/manage-astronaut/${this.loadedPerson.id}`]);
        this.notificationService.notify(`Created new Duty for ${this.loadedPerson.name}: ${this.dutyName}!`)
      },
      error: response => {
        this.notificationService.notifyErrorResponse(response.error);
      }
    });
  }
}
