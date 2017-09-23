﻿module Types

    // ---------- Types -----------------

    // tuple
    let tuple = (1,2)
    let (x1,x2) = tuple

    // List (Single Linked list)
    let list1 = [1;2;3]
    let list2 = [for i in 0..10 -> i]
    
    // Array
    let arr = [|1;2;3|]

    // Lazy Sequence (aka IEnumerable)
    let sequence = seq { for i in 1..100 -> i }

    // option
    let op1 = Some 10
    let op2 = None
    
    // Record - immutable data types
    type Contact = { Name: string; Email: string; Phone: string option}
    
    //x: Record
    let x = { Name = "Oleg"; Email = "o@i.com"; Phone = None }
    let y = { Name = "Oleg"; Email = "o@i.com"; Phone = None }
    let areEqual = x = y // true

    let copy = { x with Phone = Some "097 100 10 10"}