// ------ Structure----------------

namespace Fsharp.Syntax

open System
open System.Text

module Basics = 
    open System.Security.Cryptography.X509Certificates

// ------ Value ----------------
    let date = new DateTime(2001, 2, 3)
    let anInt = 1
    let anotherInt: int = 1

    // Strict types:

    let float1 = 1.0
    
    //let x:float = 1  <--- won't compile
    let float2: float = float 1;

    let x = 1
    let mutable y = 2
    y <- 23

// ------ Functions ----------------

    //  int -> int
    let f1 x = x*x
    let f2 = fun x -> x*x

    /// int -> int -> int
    let add x y = x + y

    // int -> unit  (aka void)

    let printSquare x = 
        let square = x * x
        printfn "%d" square
    

    // Functional composition

    let square x = x * x
    let print x =  printfn "%d" x
    let printSquare1 x = print (square x)
    // or
    let printSquare2 x = square x |> print 
    // or
    let printSquare3 x = 
        x |> square 
          |> print 
    // or
    let printSquare4 = square >> print 

    // -------------- infix functions (aka operators)-----------------
    let ( --> ) x y = Math.Pow(x, y) 
    let pow2_10 = 2.0 --> 10.0
    let pow2_10_1 = (-->) 2.0 10.0
   
    let rec factorial n = match n with
        | 0 | 1 -> 1
        | x ->  n * factorial (n-1) 
    let (!) = factorial
