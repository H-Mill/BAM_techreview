import { Component, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PersonService } from '../services/person/person.service';
import { NotificationService } from '../services/notification/notification.service';
import { Person } from '../models/person/person';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AstronautDetail } from '../models/astronautDetail/astronautDetail';
import { UpdatePerson } from '../models/person/updatePersonRequest';

@Component({
  selector: 'app-manage-astronaut',
  templateUrl: './manage-astronaut.component.html',
  styleUrl: './manage-astronaut.component.scss'
})
export class ManageAstronautComponent {
  loadedPerson: Person = <Person>{}
  name: string = ''
  careerStartDate: Date = new Date()
  currentRank: string = ''

  constructor(
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private personService: PersonService,
    private notificationService: NotificationService) { }

  ngOnInit() {
    this.loadExistingAstronaut();
  }

  loadExistingAstronaut(): void {
    this.activatedRoute.params.subscribe(params => {
      const personId = params['personId'];
      if (personId === undefined) return;

      this.personService.getPersonById(personId).subscribe({
        next: (data) => {
          this.loadedPerson = data.person;
          if (this.loadedPerson != null) {
            this.name = this.loadedPerson.name;

            if (this.loadedPerson.astronautDetail != null) {
              this.careerStartDate = this.loadedPerson.astronautDetail.careerStartDate ?? new Date();
              this.currentRank = this.loadedPerson.astronautDetail.currentRank ?? '';
            }
          }

        },
        error: (response) => {
          this.notificationService.notifyErrorResponse(response.error);
        }
      });
    });
  }

  submit(): void {

    const updatedPerson: UpdatePerson = {
      id: this.loadedPerson?.id,
      name: this.name,
      astronautDetail: {
        currentRank: this.currentRank,
        careerStartDate: this.careerStartDate
      }
    };

    this.personService.addOrUpdatePerson(updatedPerson).subscribe({
      next: (response) => {
        this.router.navigate(['/manage-astronauts']);
        this.notificationService.notify(`Updated ${updatedPerson.name}!`)
      },
      error: (response) => {
        this.notificationService.notifyErrorResponse(response.error);
      }
    });
  }
}
