module Logic

open System

open StateChecker
open Model

let getNextMove (moves:MoveViewModel list) = 
    match moves with 
    | head::tail -> (Some moves.Head, moves.Tail)
    | [] -> (None, []) //using moves instead of [] would avoid creating a new object

let getAllMoves_FromViewModel ():MoveViewModel list = 
    [
        {
            CellFrom = (E, Two);
            CellTo = (E, Four);
        };
        {
            CellFrom = (E, Seven);
            CellTo = (E, Six);
        }
    ]

let getAllMoves_FromModel () =
    let model = {
        Player = White;
        CellTo = (A, Three);
        PieceMoved = PieceFactory.createPiece Knight (A,Two)
        PieceCaptured = None
    }
    model

let private extractParseResult parseResult = 
    match parseResult with 
    | Ok moves -> Ok {
            Moves = moves;
        }
    | Error invalidMoves -> Error {
            ErrorMessage = "found invalid moves in move text";
            InvalidMoves = invalidMoves;
        }  

let convertMovesToViewModels board (moves:(Move*Move) list) = 
    printfn "moves parsed from text: %A" moves
    //step 1: flatten move list
    //step 2: iterate over flattened list with recursive function
    //  * each iteration needs to produce a new dictionary for the player's moves
    //  * each iteration produces 1 ViewModel
    //  * return value is concatenated list of ViewModels
    Ok {
        Moves = []
    }

let getAllMoves_FromText board = 
    let moveText = Data.loadAllMoves "pgns/example.pgn"
    let parseResult = Parser.parseMoveText moveText
    //we can't use StateChecker.CheckState here, b/c we want to create a different kind of error instead of returning the original error back
    let handledParseResult = 
        match parseResult with 
        | Ok moves -> Ok moves
        | Error invalidMoves -> Error {
                ErrorMessage = "found invalid moves in move text";
                InvalidMoves = invalidMoves;
            }  

    let retVal = 
        handledParseResult
        |> checkState (convertMovesToViewModels board)

    retVal    

(*
v0

only supporting regular moves:
    * e.g., move piece without capture, caslting, etc.
    * not supporting moves that need to clarify which Bishop or Knight was moved
    * only supporting Next Turn = white (1 dot after number)


*)