module PieceFactory

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

let getDefaultStartingPoints destPoint = 
    []

let createKnight () = {
    PieceType = Knight;
    GetLegalStartingPoints = getKnightLegalStartingPoints
}

let getLegalStartingPoints pieceType = 
    match pieceType with 
    | Knight -> getKnightLegalStartingPoints
    | _ -> getDefaultStartingPoints

let createPiece pieceType = {
    PieceType = pieceType;
    GetLegalStartingPoints = getLegalStartingPoints pieceType
}