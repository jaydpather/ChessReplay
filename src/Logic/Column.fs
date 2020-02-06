module Column

open System 

open Model

let fromInt i = 
    match i with 
    | 1 -> Some A
    | 2 -> Some B
    | 3 -> Some C
    | 4 -> Some D
    | 5 -> Some E
    | 6 -> Some F
    | 7 -> Some G
    | 8 -> Some H
    | _ -> None

let toInt column = 
    match column with
    | A -> 1
    | B -> 2
    | C -> 3
    | D -> 4
    | E -> 5
    | F -> 6
    | G -> 7
    | H -> 8

let fromString str = 
    match str with 
    | "a" -> Some A
    | "b" -> Some B
    | "c" -> Some C
    | "d" -> Some D
    | "e" -> Some E
    | "f" -> Some F
    | "g" -> Some G
    | "h" -> Some H
    | _ -> None

let fromValidString str = 
    match str with 
    | "a" -> A
    | "b" -> B
    | "c" -> C
    | "d" -> D
    | "e" -> E
    | "f" -> F
    | "g" -> G
    | "h" -> H
    | s -> String.Format("{0} is not a valid column", s) |> failwith 