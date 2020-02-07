module PieceFactory

open System

open Model


let getCoordinateFromInts intTuple = 
    let (columnInt, rowInt) = intTuple
    let columnOpt = Column.fromInt columnInt
    let rowOpt = Row.fromInt rowInt

    match (columnOpt, rowOpt) with 
    | (Some(c), Some(r)) -> Some (c, r)
    | (None, _) -> None
    | (_, None) -> None

let filterNones (lst:Coordinate list) (item:Coordinate option) = 
    match item with 
    | Some i -> i :: lst
    | None -> lst

let getKnightLegalStartingPoints destPoint = 
    let (column, row) = destPoint
    let columnInt = Column.toInt column
    let rowInt = Row.toInt row

    //todo: create these coordinates programatically
    // 2U, 1R
    let upRight = (columnInt + 1, rowInt + 2)
    // 2U, 1L
    let upLeft = (columnInt - 1, rowInt + 2)
    // 2D, 1R
    let downRight = (columnInt + 1, rowInt - 2)
    // 2D, 1L
    let downLeft = (columnInt - 1, rowInt - 2)
    // 2R, 1U
    let rightUp = (columnInt + 2, rowInt + 1)
    // 2R, 1D
    let rightDown = (columnInt + 2, rowInt - 1)
    // 2L, 1U
    let leftUp = (columnInt - 2, rowInt + 1)
    // 2L, 1D
    let leftDown = (columnInt - 2, rowInt - 1)

    let intTuples = [upRight; upLeft; downRight; downLeft; rightUp; rightDown; leftUp; leftDown]
    let coordOpts = List.map getCoordinateFromInts intTuples
    let coords = List.fold filterNones [] coordOpts
    coords

let isLegalMove_Knight source dest = 
    let (iSourceCol, iSourceRow) = Coordinate.toInts source
    let (iDestCol, iDestRow) = Coordinate.toInts dest
    
    let colDiff = iDestCol - iSourceCol
    let rowDiff = iDestRow - iSourceRow

    let absColDiff = Math.Abs(colDiff)
    let absRowDiff = Math.Abs(rowDiff)

    let sum = absColDiff + absRowDiff

    sum = 3

let isLegalMove_Default source dest =
    true

let getIsLegalMoveFunc pieceType = 
    match pieceType with 
    | Knight -> isLegalMove_Knight
    | _ -> isLegalMove_Default

let createPiece_Old pieceType = {
    PieceType = pieceType;
    Position = (A, Three);
    IsLegalMove = isLegalMove_Default;
}

let createPiece pieceType position = {
    PieceType = pieceType;
    Position = position;
    IsLegalMove = getIsLegalMoveFunc pieceType
}