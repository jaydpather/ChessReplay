module Logic

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

let private getViewModelFromModel model =
    let legalStartingPoints = model.PieceMoved.GetLegalStartingPoints model.CellTo
    legalStartingPoints
    // let viewModel = {
    //     CellTo = model.CellTo;
    //     CellFrom = 
    // }

let getAllMoves_FromModel () =
    let model = {
        Player = White;
        CellTo = (A, Three);
        PieceMoved = PieceFactory.createKnight ();
        PieceCaptured = None
    }
    getViewModelFromModel model


let getAllMoves_FromText () = 
    let moveText = Data.loadAllMoves "pgns/example.pgn"
    Parser.parseMoveText moveText


(*
v0

only supporting regular moves:
    * e.g., move piece without capture, caslting, etc.
    * not supporting moves that need to clarify which Bishop or Knight was moved
    * only supporting Next Turn = white (1 dot after number)


*)