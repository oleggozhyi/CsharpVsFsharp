// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

open ComputationExpressions

[<EntryPoint>]
let main argv = 
    saveNewUser "Name" "email@domain.com" |> ignore

    printfn "%A" argv
    0 // return an integer exit code
