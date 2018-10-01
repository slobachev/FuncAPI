namespace FuncAPI.Http

open Giraffe
open Microsoft.AspNetCore.Http
open FuncAPI

module TodoHttp =
  let handlers : HttpFunc -> HttpContext -> HttpFuncResult =
    choose [
      POST >=> route "/todos" >=>
        fun next context ->
          text "Create" next context

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
          text ("Delete " + id) next context)
    ]