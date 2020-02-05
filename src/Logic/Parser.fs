module Parser

open System.Text.RegularExpressions
open System

//todo: do we even need this function? seems like it only returns Matches where Success = true
let private filterMatch lstResults (curMatch:Match) = 
    match curMatch.Success with
    | true -> curMatch.Value :: lstResults
    | false -> lstResults

(*
PLAN OF ACTION
    * make StateChecker function. (use built-in .NET result)
    * new function: validateMoveText
        * returns Success of string list or Failure of string list
        * Logic layer checks for success/failure and returns new state:
            * ErrorMessage (string)
            * btnNextMoveEnabled (bool)
        * App.fs updates btnNextMove and lblErrMsg. (new file for this?)        
    * new function: parseMoveText
        * takes a string list
        * returns a Move list    
*)

let getInvalidMoves regexPattern moveText  = 
    let splitMoveText = Regex.Split(moveText, regexPattern)
    let filterBlankStrings = String.IsNullOrEmpty >> not 
    let invalidMoveStrings = 
        Array.filter filterBlankStrings splitMoveText
        |> Array.toList
    invalidMoveStrings

let getValidMoves regexPattern moveText = 
    let matches = Regex.Matches(moveText, regexPattern)
    let matchList = 
        matches
        |> Seq.cast
        |> List.ofSeq

    let validMatches = List.filter (fun (x:Match) -> x.Success) matchList
    let validMoveStrings = List.map (fun (x:Match) -> x.Value) validMatches
    validMoveStrings


let parseMoveText moveText = 
    (*Currently supporting only these moves:
        * next turn is white
        * no captures
        * no castling
        * no check or checkmate
    *)

    let regexPattern = "\d\. [KQRBN]{0,1}[a-h][1-8] [KQRBN]{0,1}[a-h][1-8] "
    let invalidMoveStrings = getInvalidMoves regexPattern moveText
    let getValidMovesFunc = fun () -> getValidMoves regexPattern moveText

    let retVal = 
        match invalidMoveStrings.Length with 
        | 0 -> getValidMovesFunc () |> Ok 
        | numErrors -> invalidMoveStrings |> Error

    retVal


//K (king), Q (queen), R (rook), B (bishop), and N (knight).
