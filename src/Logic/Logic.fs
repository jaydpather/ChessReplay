module Logic

open Model


let getNextMove_Old () = {
        Player = White;
        PieceMoved = Pawn;
        PieceCaptured = None;
        CellFrom = (E, Two);
        CellTo = (E, Four);
    }

let getNextMove (moves:Move list) = 
    match moves with 
    | head::tail -> (Some moves.Head, moves.Tail)
    | [] -> (None, []) //using moves instead of [] would avoid creating a new object

let getAllMoves () = 
    [
        {
            Player = White;
            PieceMoved = Pawn;
            PieceCaptured = None;
            CellFrom = (E, Two);
            CellTo = (E, Four);
        };
        {
            Player = Black;
            PieceMoved = Pawn;
            PieceCaptured = None;
            CellFrom = (E, Seven);
            CellTo = (E, Six);
        }
    ]
