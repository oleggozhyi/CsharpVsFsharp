module Patterns

    // Match on list
    let listAnalyzer list = 
        match list with
        | [] -> printf "empty list"
        | [_] -> printf "list with exactly one element"
        | 1::tail -> printf "list with 2 of more elements starting from 1"
        | _::x::tail when x > 2 -> printf "[any,2+,any..]"
        | head::tail ->  printf "other"

     //match on discriminated union
    type Shape =
        | Circle of radius: float
        | Rectangle of width: float * heihgt: float
        | Square  of width: float
    let area shape =
        match shape with 
        | Circle radius -> System.Math.PI * radius * radius
        | Square width -> width * width
        | Rectangle (width, hight) -> width * hight

    //match on tuple
    let strToInt s =
        match  System.Int32.TryParse(s) with
        | true, x -> Some x
        | false, _ -> None
    
    // match on record
    type Contact = { Name: string; Email: string; Phone: string option }
    let isJohn contact = 
        match contact with 
        | { Name = "john" } -> true
        | _ -> false
    
    // match on class
    type Animal(Name: string) =
        member x.Name with get() = Name
    type Dog(Name: string, Kind: string) = 
        inherit Animal(Name)
        member x.Bark() = "Gav"
        
        member x.Kind with get() = Kind

    type Bird(Name: string) = 
        inherit Animal(Name)
        member x.Tweet() = "phew-phew"
    
    let makeSound (an: Animal) = 
        match an with
        | :? Dog as d -> printfn "Dog of Kind %s named %s says %s" d.Name d.Kind (d.Bark())
        | :? Bird as b -> printfn "Bird named %s says %s" b.Name (b.Tweet())
        | _ -> failwith "unkown Animal"
 