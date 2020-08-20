import { Component } from '@angular/core';
import { Driver } from '../models/driver.model';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {
  public drivers:Driver[];
  public uploadFinished = (event:Driver[]) => {
    console.log(event);
    this.drivers = event;
  }
}


