module PieceFactory

// open Model

// //todo: make this function part of the column type? (rename to toInt
// //todo: make row and column enums that map to ints
// let getIntFromColumn column = 
//     match column with
//     | A -> 1
//     | B -> 2
//     | C -> 3
//     | D -> 4
//     | E -> 5
//     | F -> 6
//     | G -> 7
//     | H -> 8

// let getIntFromRow row = 
//     match row with 
//     | One -> 1
//     | Two -> 2
//     | Three -> 3
//     | Four -> 4
//     | Five -> 5
//     | Six -> 6
//     | Seven -> 7
//     | Eight -> 8

// let getCoordinateFromValidInts columnInt rowInt = 
//     let column = match columnInt with 
//         | 1 -> A
//         | 2 -> B
//         | 3 -> C
//         | 4 -> D
//         | 5 -> E
//         | 6 -> F
//         | 7 -> G
//         | 8 -> H

//     let row =  match rowInt with 
//         | 1 -> One
//         | 2 -> Two
//         | 3 -> Three
//         | 4 -> Four
//         | 5 -> Five
//         | 6 -> Six
//         | 7 -> Seven
//         | 8 -> Eight

//     (column, row)

// let getCoordinateFromInts intTuple =
//     match intTuple with 
//     | (columnInt, _) when columnInt < 1 -> None
//     | (columnInt, _) when columnInt > 8 -> None
//     | (_, rowInt) when rowInt < 1 -> None
//     | (_, rowInt) when rowInt > 8 -> None
//     | (columnInt, rowInt) -> Some getCoordinateFromValidInts columnInt rowInt




// let getKnightLegalStartingPoints destPoint = 
//     let (column, row) = destPoint
//     let columnInt = getIntFromColumn column
//     let rowInt = getIntFromRow row

//     // 2U, 1R
//     let upRight = (columnInt + 1, rowInt + 2)
//     // 2U, 1L
//     let upLeft = (columnInt - 1, rowInt + 2)
//     // 2D, 1R
//     let downRight = (columnInt + 1, rowInt - 2)
//     // 2D, 1L
//     let downLeft = (columnInt - 1, rowInt - 2)
//     // 2R, 1U
//     let rightUp = (columnInt + 2, rowInt + 1)
//     // 2R, 1D
//     let rightDown = (columnInt + 2, rowInt - 1)
//     // 2L, 1U
//     let leftUp = (columnInt - 2, rowInt + 1)
//     // 2L, 1D
//     let leftDown = (columnInt - 2, rowInt - 1)

//     [upRight, upLeft, downRight, downLeft, rightUp, rightDown, leftUp, leftDown]




// let createKnight () = {
//     PieceType = Knight;
//     GetLegalStartingPoints = getKnightLegalStartingPoints
// }