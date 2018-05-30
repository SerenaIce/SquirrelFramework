SquirrelFramework
=================
Squirrel Framework - A lightweight back-end framework and utilities kit based on MongoDB/Redis.

Get package
------------
You can get the latest stable release from the `[official Nuget.org feed](https://www.nuget.org/packages/SquirrelFramework)`

https://www.nuget.org/packages/SquirrelFramework

## Getting Started
1. Create a .NET project, please ensure that the target framework `MUST be .NET Framework 4.6.2 or later`.

2. Get the Nuget package by searcing the keyword "SquirrelFramework" or using the Project Manager:

Install-Package SquirrelFramework -Version 1.0.13

3. Create your Domain Model
```C#
    using SquirrelFramework.Domain.Model;
```
```C#
    [Database("YourDatabaseName")]
    [Collection("UsersCollectionName")]
    public class User : DomainModel
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
    }
```
`The [Database] attribute is not necessary`, you can set the default MongoDB database name at /Config/mongodb.config

4. Create your Repository for MongoDB CRUD

```C#
    using SquirrelFramework.Repository;
```
```C#
    public class UserDBRepo: RepositoryBase<User> {}
```
`or`
```C#
    public class UserDBRepo: CustomizedRepositoryBase<User> {}
```

5. Now you are free to perform various operations on MongoDB.

* Add a new user record

* Get all users who are older than 18 and are two kilometers away from me

* Bulk delete users

* Get users by pager


6. Create your Domain Service (This is not a necessary step)



Contributing
------------

* Hendry Qi              me@nap7.com
