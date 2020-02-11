module CoordCalc

open System
open Model

let isLegalMove_Knight dest source = 
    let (iSourceCol, iSourceRow) = Coordinate.toInts source
    let (iDestCol, iDestRow) = Coordinate.toInts dest
    
    let colDiff = iDestCol - iSourceCol
    let rowDiff = iDestRow - iSourceRow

    let absColDiff = Math.Abs(colDiff)
    let absRowDiff = Math.Abs(rowDiff)

    let sum = absColDiff + absRowDiff

    sum = 3

let isLegalMove_Default dest source =
    true

let isLegalMove dest piece = 
    match piece.PieceType with 
    | Knight -> isLegalMove_Knight dest piece.Position
    | _ -> isLegalMove_Default dest piece.Position

let recordLegalMove dest piece = 
    (piece, isLegalMove dest piece)

let findLegalPieceMoved possiblePieces dest =
    let areLegalMoves = List.map (recordLegalMove dest) possiblePieces
    let checkLegalMove recordedLegalMove = 
        let (_, isLegal) = recordedLegalMove
        isLegal
    let legalMoves = List.filter (checkLegalMove) areLegalMoves

    let (piece, _) = legalMoves.Head //todo: handle error case, where list has more than 1 item
    piece

let convertToViewModel move (playerPieces:Map<PieceType, Piece list>) = 
    //this func needs to determine the source coordinate of the move
    //to do that, we need to determine which Piece object was moved.
    //  * the player could have 2 Knights, 2 Bishops, etc.
    //  * there should only be 1 piece of each type that is in a legal position to move  to the destination
    //  * in the case that the player only has 1 Knight, Bishop, etc., then we automatically know which piece was moved
    let possiblePieces = playerPieces.[move.PieceTypeMoved]
    let pieceMoved =  
        match possiblePieces.Length with 
        | 1 -> possiblePieces.Head
        //todo: handle case of 0. (error case)
        | _ -> findLegalPieceMoved possiblePieces move.CellTo

    {
        CellFrom = pieceMoved.Position
        CellTo = move.CellTo
    }
