module Logic

open Model


let getNextMove () = {
        Player = White;
        PieceMoved = Pawn;
        PieceCaptured = None;
        CellFrom = (E, Two);
        CellTo = (E, Four);
    }
