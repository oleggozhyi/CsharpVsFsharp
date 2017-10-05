module StringToInt

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
    member __.Bind(f,x) = Option.bind
    member __.Return(x) = Some

let maybe = new MaybeBuilder();
     
let strToIntFold s =
    let reducer acc ch = 
        match acc with 
        | None -> None
        | Some accValue -> match charToDigit ch with 
                | None -> None
                | Some i -> accValue * 10 + i |> Some 
    s |> Seq.fold reducer (Some 0)
        

let strToUnsignedInt s =
    let reducer acc ch = 
        match acc with 
        | None -> None
        | Some accValue -> match charToDigit ch with 
                | None -> None
                | Some i -> accValue * 10 + i |> Some 
    s |> Seq.fold reducer (Some 0)
        
let getSignAndUnsignedString (s: string) = 
    if s.Length > 1 && s.Chars(0) = '-' then -1, (s.Substring (1)) 
    else 1, s


let strToSignedInt s = 
    let sign, unsignedString = getSignAndUnsignedString s
    sign * (strToUnsignedInt unsignedString)


    

