module ComputationExpressions
    // In FP, you should not be throwing exceptions 
    type Result<'a,'b> =
        | Success of 'a
        | Failure of 'b
    type Email = Email of string
    type User = { Id: int option; Name: string; Email: Email }

    let tryParseEmail email = match email with
        | s when String.length s > 100 -> Failure "too long email"
        | s when  s.Contains("@") = false -> Failure "Should have @"
        | s -> Email email |> Success
    
    let createUser name email = Success { Name = name; Email = email; Id = None }
  
    let saveToDb user = 
        try
            // emulating saving to db
            let generatedId  = 42
            Success { user with Id = Some generatedId }
        with
            | ex -> Failure (ex.ToString())
    
    // now lets create an ORCHESTRATOR (actually, composition)
    let saveNewUserStraightfowrard name email = 
        match tryParseEmail email with 
            | Failure error -> Failure error
            | Success validatedEmail -> 
                match createUser name validatedEmail with
                     | Failure error -> Failure error
                     | Success user -> saveToDb user 
             

    // Monads (aka Computation expressions) to rescue!
    let bind x f =
        printfn "-------> %A " x
        match x with 
        | Success s -> f s
        | Failure f -> Failure f
    let (>>=) x f = bind x f
    let (>=>) f1 f2 x = f1 x >>= f2
    type ResultBuilder() = 
        member this.Bind(x, f) = x >>= f
        member this.Return x = Success x

    let result = ResultBuilder()

    let saveNewUser name email = 
        result {
            let! validatedEmail = tryParseEmail email
            let! user = createUser name validatedEmail 
            let! savedUser = saveToDb user
            return  savedUser
        }
     
    let saveNewUserDesugared1 name email = 
        tryParseEmail email
        >>= (createUser name)
        >>= saveToDb

    let saveNewUserDesugared2 name = 
        tryParseEmail 
        >=> (createUser name)
        >=> saveToDb
     