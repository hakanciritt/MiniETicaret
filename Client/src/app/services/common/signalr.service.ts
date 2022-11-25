import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  constructor() { }
  private _connection: HubConnection;
  get connection(): HubConnection {
    return this._connection;
  }

  start(hubUrl: string) {
    if (!this._connection || this._connection?.state == HubConnectionState.Disconnected) {
      const builder: HubConnectionBuilder = new HubConnectionBuilder();

      const hubConnection: HubConnection = builder.withUrl(hubUrl).withAutomaticReconnect().build();

      hubConnection.start()
        .then(() => {
          console.log("connected");
        })
        .catch(error => {
          setTimeout(() => this.start(hubUrl), 2000);
        });

      this._connection = hubConnection;

    }
    this._connection.onreconnected(connectionId => {
      console.log(connectionId);

    })
    this._connection.onreconnecting(err => { console.log(err) });
    this._connection.onclose(error => { console.log(error) });
  }

  invoke(procedureName: string, message: any, successCallBack?: (value) => void, errorCallBack?: (error) => void) {

    this.connection.invoke(procedureName, message)
      .then(successCallBack)
      .catch(errorCallBack)

  }

  on(procedureName: string, callBack: (...message: any) => void) {
    this.connection.on(procedureName, callBack);

  }

}
