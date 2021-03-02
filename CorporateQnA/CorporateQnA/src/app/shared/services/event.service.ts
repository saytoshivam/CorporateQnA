import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class EventService {
  public behaviourSubject = new BehaviorSubject<any>('');
  constructor() { }

  emit<T>(data: T) {
    this.behaviourSubject.next(data);
  }

  on<T>(): Observable<T> {
    return this.behaviourSubject.asObservable();
  }
}
