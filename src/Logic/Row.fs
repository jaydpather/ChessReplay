module Row

open System

open Model

let fromInt i = 
    match i with 
    | 1 -> Some One
    | 2 -> Some Two
    | 3 -> Some Three
    | 4 -> Some Four
    | 5 -> Some Five
    | 6 -> Some Six
    | 7 -> Some Seven
    | 8 -> Some Eight
    | _ -> None

let toInt row = 
    match row with 
    | One -> 1
    | Two -> 2
    | Three -> 3
    | Four -> 4
    | Five -> 5
    | Six -> 6
    | Seven -> 7
    | Eight -> 8

let fromString str = 
    match str with 
    | "1" -> Some One
    | "2" -> Some Two
    | "3" -> Some Three
    | "4" -> Some Four
    | "5" -> Some Five
    | "6" -> Some Six
    | "7" -> Some Seven
    | "8" -> Some Eight
    | _ -> None

let fromValidString str = 
    match str with 
    | "1" -> One
    | "2" -> Two
    | "3" -> Three
    | "4" -> Four
    | "5" -> Five
    | "6" -> Six
    | "7" -> Seven
    | "8" -> Eight
    | s -> String.Format("{0} is not a valid Row", s) |> failwith 