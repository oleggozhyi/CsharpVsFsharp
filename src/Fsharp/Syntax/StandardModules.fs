module PatternMatching

    let listOfInts = [1,2,4,5,6,7,8]

    let sumOfEven list = 
        list |> List.where (fun x -> x % 2 = 0)
             |> List.sum

    let rec fib = function
        | 0 | 1 -> 1I
        | n -> fib (n-1) + fib (n-2)

    let sumFibNumbers count = 
        Seq.initInfinite fib
        |> Seq.take count
        |> Seq.reduce (+)