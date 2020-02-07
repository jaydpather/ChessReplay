module Parser

open System.Text.RegularExpressions
open System

open Model

//todo: split validation an parsing into 2 files

let private getInvalidMoves regexPattern moveText  = 
    let splitMoveText = Regex.Split(moveText, regexPattern)
    let filterBlankStrings = String.IsNullOrEmpty >> not 
    let invalidMoveStrings = 
        Array.filter filterBlankStrings splitMoveText
        |> Array.toList
    invalidMoveStrings

let private getValidMoves regexPattern moveText = 
    let matches = Regex.Matches(moveText, regexPattern)
    let matchList = 
        matches
        |> Seq.cast
        |> List.ofSeq

    let validMatches = List.filter (fun (x:Match) -> x.Success) matchList
    let validMoveStrings = List.map (fun (x:Match) -> x.Value) validMatches
    validMoveStrings

let patternPiece = "[KQRBN]{0,1}"
let patternColumn = "[a-h]"
let patternRow = "[1-8]"
let patternPlayerMove = String.Format("{0}{1}{2}", patternPiece, patternColumn, patternRow)

let parsePlayerMoveString player playerMoveString = 
    //todo: use Match.Captures property to get all parts of string at once
    let pieceTypeStr = Regex.Match(playerMoveString, patternPiece).Value
    let toColumnStr = Regex.Match(playerMoveString, patternColumn).Value
    let toRowStr = Regex.Match(playerMoveString, patternRow).Value

    //todo: make these fromString methods throw exceptions instead of returning Option
    //  then you don't have to deal with all this optional stuff
    let pieceType = PieceType.fromValidString pieceTypeStr
    let column = Column.fromValidString toColumnStr
    let row = Row.fromValidString toRowStr

    {
        Player = player;
        PieceMoved = PieceFactory.createPiece_Old pieceType
        CellTo = (column, row);
        PieceCaptured = None
    }


let parseMoveString move = 
    //todo: cleaner to use Regex.Matches instead of substring. (used substring b/c we don't have Match.NextMatch)
    let firstMatch = Regex.Match(move, patternPlayerMove)
    let firstMoveString = firstMatch.Value
    
    //we have to do a substring here b/c it seems like JS does not support Match.NextMatch
    let startIndex = move.IndexOf(firstMoveString) + firstMoveString.Length
    let remainingText = move.Substring(startIndex)
    let secondMatch = Regex.Match(remainingText, patternPlayerMove)
    let secondMoveString = secondMatch.Value
    
    (firstMoveString, secondMoveString)

let parseValidatedMoves validMoveStrings = 
    let parsePairOfPlayerMoveStrings pair = 
        let (first, second) = pair
        let whiteMove = parsePlayerMoveString White first
        let blackMove = parsePlayerMoveString Black second
        (whiteMove, blackMove)

    let movePairs = 
        List.map parseMoveString validMoveStrings
        |> List.map parsePairOfPlayerMoveStrings        
    
    Ok movePairs

let checkState nextFunc state = 
    match state with 
    | Ok o -> nextFunc o
    | Error e -> Error e


let validateMoveText moveText = 
    let regexPattern = String.Format("\d\. {0} {1} ", patternPlayerMove, patternPlayerMove)
    let invalidMoveStrings = getInvalidMoves regexPattern moveText

    let validateResult = 
        match invalidMoveStrings.Length with 
        | 0 -> getValidMoves regexPattern moveText |> Ok 
        | _ -> invalidMoveStrings |> Error

    validateResult    

let parseMoveText moveText = 
    (*Currently supporting only these moves:
        * next turn is white
        * no captures
        * no castling
        * no check or checkmate
    *)
    try
        let parseResult = 
            validateMoveText moveText
            |> checkState parseValidatedMoves 

        parseResult
    with 
    | ex -> Error ["unknown error parsing moveText"; ex.Message; ex.StackTrace;]