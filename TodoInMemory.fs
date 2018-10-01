module FuncAPI.TodoInMemory

open FuncAPI
open Microsoft.Extensions.DependencyInjection
open System.Collections

let find (inMemory : Hashtable) (criteria : TodoCriteria) : Todo[] =
    match criteria with
    | All -> inMemory.Values |> Seq.cast |> Array.ofSeq

let save (inMemory : Hashtable) (todo: Todo) : Todo =
    inMemory.Add(todo.Id, todo) |> ignore
    todo