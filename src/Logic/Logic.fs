module Logic

open Model


let getNextMove () = {
        Player = White;
        PieceMoved = Pawn;
        PieceCaptured = None;
        CellFrom = (E, Two);
        CellTo = (E, Four);
    }


// let getAllMoves () = 
//     [
//         {
//             Player = White;
//             PieceMoved = Pawn;
//             PieceCaptured = None;
//             CellFrom = (E, Two);
//             CellTo = (E, Four);
//         },
//         {
//             Player = Black;
//             PieceMoved = Pawn;
//             PieceCaptured = None;
//             CellFrom = (E, Two);
//             CellTo = (E, Four);
//         }
//     ]
