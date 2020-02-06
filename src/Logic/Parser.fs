module Parser

open System.Text.RegularExpressions
open System

open Model

//todo: do we even need this function? seems like it only returns Matches where Success = true
let private filterMatch lstResults (curMatch:Match) = 
    match curMatch.Success with
    | true -> curMatch.Value :: lstResults
    | false -> lstResults

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
    let pieceTypeStr = Regex.Match(playerMoveString, patternPiece).Value
    let toColumnStr = Regex.Match(playerMoveString, patternColumn).Value
    let toRowStr = Regex.Match(playerMoveString, patternRow).Value

    //todo: make these fromString methods throw exceptions instead of returning Option
    //  then you don't have to deal with all this optional stuff
    let pieceTypeOpt = PieceType.fromString pieceTypeStr
    let toColumnOpt = Column.fromString toColumnStr
    let toRowOpt = Row.fromString toRowStr

    let playerMove =
        match (pieceTypeOpt, toColumnOpt, toRowOpt) with 
        | (None, _, _) -> None
        | (_, None, _) -> None
        | (_, _, None) -> None
        | (Some pieceType, Some column, Some row) -> Some {
                Player = player;
                PieceMoved = PieceFactory.createPiece pieceType
                CellTo = (column, row);
                PieceCaptured = None
            }
    playerMove


let parseMoveString move = 
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

    let moveOptPairs = 
        List.map parseMoveString validMoveStrings
        |> List.map parsePairOfPlayerMoveStrings        

    let findSomes = fun lst pair ->
        match pair with 
        | (Some x, Some y) -> (x,y) :: lst
        | (_, _) -> lst

    let movePairs = 
        List.fold findSomes [] moveOptPairs
        |> List.rev

    match movePairs.Length = moveOptPairs.Length with
    | true -> movePairs |> Ok
    | false -> Error ["could not parse from validated moves"] //todo: how to report this error? how to include invalid moves?

let checkState nextFunc state = 
    match state with 
    | Ok o -> nextFunc o
    | Error e -> Error e

let parseMoveText moveText = 
    (*Currently supporting only these moves:
        * next turn is white
        * no captures
        * no castling
        * no check or checkmate
    *)

    let regexPattern = String.Format("\d\. {0} {1} ", patternPlayerMove, patternPlayerMove)
    let invalidMoveStrings = getInvalidMoves regexPattern moveText

    let validateResult = 
        match invalidMoveStrings.Length with 
        | 0 -> getValidMoves regexPattern moveText |> Ok 
        | _ -> invalidMoveStrings |> Error
    
    let parseResult = 
        validateResult
        |> checkState parseValidatedMoves 

    parseResult