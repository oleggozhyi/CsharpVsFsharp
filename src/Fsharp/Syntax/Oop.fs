module Oop
    // extension 
    type System.String with 
        member x.ToInt() = System.Int32.Parse(x)
             
    let x = "123".ToInt()

    // Class
    type Rectangle(x:int, y:int) =
        class 
            static member Create(x,y) = Rectangle(x = x, y = y)   

            member this.Area() = x * y 

            member this.Sum 
                with get() = x + y
                and set(a: int) = a |> ignore
        end

    

