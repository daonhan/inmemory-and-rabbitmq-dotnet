Project made for study propose.

Backend made with .NET 5, implementing a system that has two database, one for read (Cache) and one for write (Data), so firstly we add/update/remove Vehicle from Data database, after that dispatch a event for RabbitMq that is processed by Receiver, who add/update/remove Vehicle from Cache.

Frontend made with Angular 12, have two pages, Home and Vehicles. Vehicles has the list of all the Vehicles and options to add/update/remove a Vehicle, using bootstrap-modal. Also have interceptors to catch errors from backend, ngx-toastr and masks.
