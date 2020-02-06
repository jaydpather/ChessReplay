module PieceType

open System

open Model

let fromString s = 
    match s with 
    | "K" -> Some King
    | "Q" -> Some Queen
    | "R" -> Some Rook
    | "B" -> Some Bishop
    | "N" -> Some Knight
    | "" -> Some Pawn //String.Empty cannot be used in pattern matching b/c it's not a literal. WEIRD
    | _ -> None

let fromValidString s = 
    match s with 
    | "K" ->  King
    | "Q" ->  Queen
    | "R" ->  Rook
    | "B" ->  Bishop
    | "N" ->  Knight
    | "" ->  Pawn //String.Empty cannot be used in pattern matching b/c it's not a literal. WEIRD
    | other -> String.Format("{0} is not a valid piece type", other) |> failwith

    //K (king), Q (queen), R (rook), B (bishop), and N (knight).