module Parser

open System.Text.RegularExpressions

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
    let regex = "\d. \w{2,3} \w{2,3} "
    let matches = Regex.Matches(moveText, regex)

    let matchList = 
        matches
        |> Seq.cast
        |> List.ofSeq
    let results = List.fold filterMatch [] matchList
    results


//K (king), Q (queen), R (rook), B (bishop), and N (knight).
