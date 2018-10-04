namespace FuncAPI.Http

open Giraffe
open Microsoft.AspNetCore.Http
open FuncAPI
open System
open FSharp.Control.Tasks.V2

module TodoHttp =
  let handlers : HttpFunc -> HttpContext -> HttpFuncResult =
    choose [
      POST >=> route "/todos" >=>
        fun next context ->
            task {
                let save = context.GetService<TodoSave>()
                let! todo = context.BindJsonAsync<Todo>()
                let todo = { todo with Id = ShortGuid.fromGuid(Guid.NewGuid()) }
                return! json (save todo) next context
            }

      GET >=> route "/todos" >=>
        fun next context ->
        let find = context.GetService<TodoFind>()
        let todos = find TodoCriteria.All
        json todos next context

      PUT >=> routef "/todos/%s" (fun id ->
        fun next context ->
          text ("Update " + id) next context)

      DELETE >=> routef "/todos/%s" (fun id ->
        fun next context ->
          let delete = context.GetService<TodoDelete>()
          json (delete id) next context)
    ]