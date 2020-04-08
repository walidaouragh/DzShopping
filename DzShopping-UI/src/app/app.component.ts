import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(private http: HttpClient) {

  }
  ngOnInit() {
    return this.http.get('https://localhost:5001/api/products').subscribe(p => {
      console.log('*-*-*-*-*-', p)
    })
  }

}
