import { Component, OnInit } from '@angular/core';
import { ILog } from './ILog';
import { LogService } from '../log.service';
import { AlertService } from '../../alert/alert.service';

@Component({
  selector: 'app-log',
  templateUrl: './log.component.html',
  styleUrls: ['./log.component.css']
})
export class LogComponent implements OnInit {

  logs: ILog[];
  alertService: AlertService;

  constructor(private LogService: LogService, private alert: AlertService) {
    this.alertService = alert;
  }

  ngOnInit() {
    this.cargarData();
  }

  cargarData() {
    this.LogService.getLogs()
      .subscribe(logWs => this.logs = logWs,
        error => this.alertService.ShowErrorAlert(error));
  }

}
