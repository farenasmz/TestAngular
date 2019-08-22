import { Component, OnInit } from '@angular/core';
import { ILog } from './ILog';
import { LogService } from '../log.service';

@Component({
  selector: 'app-log',
  templateUrl: './log.component.html',
  styleUrls: ['./log.component.css']
})
export class LogComponent implements OnInit {

  logs: ILog[];

  constructor(private LogService: LogService) { }

  ngOnInit() {
    this.cargarData();
  }

  cargarData() {
    this.LogService.getLogs()
      .subscribe(logWs => this.logs = logWs,
        error => console.error(error));
  }

}
