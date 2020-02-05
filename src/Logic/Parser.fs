module Parser

open System.Text.RegularExpressions
open System

//todo: do we even need this function? seems like it only returns Matches where Success = true
let filterMatch lstResults (curMatch:Match) = 
    match curMatch.Success with
    | true -> curMatch.Value :: lstResults
    | false -> lstResults

let parseMoveText moveText = 
    (*We will match the following format:
    (this is a pair of moves by white and black)
        * a digit, dot and space 
        * a group of alphanumeric chars of length 2 or 3
        * a space
        * a group of alphanumeric chars of length 2 or 3
        * a space
    *)
    //\d\. ([KQRBN]{0,1}[a-h][1-8] ){2,2}
    let regex = "\d. \w{2,3} \w{2,3} "
    let matches = Regex.Matches(moveText, regex)

    let matchList = 
        matches
        |> Seq.cast
        |> List.ofSeq

    let validMatches = List.filter (fun (x:Match) -> x.Success) matchList
    let validMoveStrings = List.map (fun (x:Match) -> x.Value) validMatches

    let splitMoveText = Regex.Split(moveText, regex)
    let filterBlankStrings = String.IsNullOrEmpty >> not 
    let invalidMoveStrings = 
        Array.filter filterBlankStrings splitMoveText
        |> Array.toList

    (validMoveStrings, invalidMoveStrings)


//K (king), Q (queen), R (rook), B (bishop), and N (knight).
