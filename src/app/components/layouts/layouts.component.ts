import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './navbar/navbar.component';

@Component({
  selector: 'app-layouts',
  standalone: true,
  // The component is standalone, meaning it can be used without being declared in an NgModule.
  // This is a feature of Angular that allows for more modular and reusable components.
  templateUrl: './layouts.component.html',
  imports: [RouterOutlet, NavbarComponent],
  styleUrls: ['./layouts.component.css'],
})
export class LayoutsComponent {}
