module Oop
    // extension 
    type System.String with 
        member x.ToInt() = System.Int32.Parse(x)
             
    let x = "123".ToInt()

    // Class

    type IHaveName = 
        abstract GetName: unit -> string

    [<AbstractClass>]
    type Shape() = 
            abstract Area: unit -> int

    type Rectangle(x:int, y:int) =
        inherit Shape()

        static member Create(x,y) = Rectangle(x = x, y = y)   
        override this.Area() = x * y 

        member this.Sum 
            with get() = x + y
            and set(a: int) = a |> ignore

        interface IHaveName with 
            override x.GetName() = "rectangle"

    

