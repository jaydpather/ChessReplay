module Logic

open Model

let getNextMove (moves:Move list) = 
    match moves with 
    | head::tail -> (Some moves.Head, moves.Tail)
    | [] -> (None, []) //using moves instead of [] would avoid creating a new object

let getAllMoves_Old () = 
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
    System.Text.RegularExpressions.Regex.Matches(moveText, regex)

let getAllMoves () = 
    let moveText = Data.loadAllMoves "pgns/example.pgn"
    parseMoveText moveText


(*
v0

only supporting regular moves:
    * e.g., move piece without capture, caslting, etc.
    * not supporting moves that need to clarify which Bishop or Knight was moved
    * only supporting Next Turn = white (1 dot after number)


*)