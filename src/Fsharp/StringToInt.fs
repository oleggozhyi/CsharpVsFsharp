module StringToInt

open ComputationExpressions

// no mutation
// no statements, only expressions
// no control flow, only data flow

let charToDigit (c: char) = 
        let d = (int c) - (int '0')
        if d >= 0 && d <= 9 then Some d else None

//let strToIntRec s =
//    let rec charsToIntRec acc = function
//        | [] -> Some acc
//        | head :: tail -> match charToDigit head with 
//            | None -> None
//            | Some i -> charsToIntRec (acc * 10 + i) tail
//    s |> List.ofSeq
//      |> charsToIntRec 0

type MaybeBuilder() = 
    member __.Bind(x, f) = Option.bind f x
    member __.Return(x) = Some x

let maybe = new MaybeBuilder();
     
let strToIntFold s =
    let reducer (acc:int option)  (ch:char) = maybe {
        let! accValue = acc
        let! digit = charToDigit ch
        return accValue * 10 + digit
      }
    s |> Seq.fold reducer (Some 0)
        

let strToUnsignedInt s =
    let reducer acc ch = 
        match acc with 
        | None -> None
        | Some accValue -> match charToDigit ch with 
                | None -> None
                | Some i -> accValue * 10 + i |> Some 
    s |> Seq.fold reducer (Some 0)
        
let getSignAndUnsignedString (s: string) = match s with
    | empty when empty.Length =0 -> None
    | s when s.Chars(0) = '-' -> (-1, s.Substring(1)) |> Some
    | s when System.Char.IsDigit(s.Chars(0)) -> (1,s) |> Some

let strToSignedInt s = 
    match getSignAndUnsignedString s with
    | None -> None
    | Some (sign, unsignedString) -> match strToUnsignedInt unsignedString with
            | None -> None
            | Some result -> result * sign |> Some
        
    
let bind (x:int option)  (f:int-> int option) = match x with 
    | None -> None
    | Some i -> f i
let (>>=) = bind

    

let strToUnsignedInt1 s =
    let reducer acc ch = 
        acc >>= fun accValue -> 
            charToDigit ch >>= fun i -> 
                accValue * 10 + i |> Some            
     
    s |> Seq.fold reducer (Some 0)