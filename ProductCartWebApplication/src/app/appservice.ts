import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';



@Injectable()



export class AppService{
    constructor(private _http :HttpClient){

    }
    get(url){
       return  this._http.get(url);
    }

}