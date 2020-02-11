module CoordCalc

open System
open Model

let getColAndRowDiff move source = 
    let dest = move.CellTo
    let (iSourceCol, iSourceRow) = Coordinate.toInts source
    let (iDestCol, iDestRow) = Coordinate.toInts dest
    
    let colDiff = iDestCol - iSourceCol
    let rowDiff = iDestRow - iSourceRow
    (colDiff, rowDiff)

let isLegalMove_Bishop move source = 
    let (colDiff, rowDiff) = getColAndRowDiff move source
    let absColDiff = Math.Abs(colDiff)
    let absRowDiff = Math.Abs(rowDiff)
    absColDiff = absRowDiff

let isLegalMove_Pawn move source = 
    let (colDiff, rowDiff) = getColAndRowDiff move source
    let colsMatch = colDiff = 0 //we aren't handling captures yet, so Pawns can only move forward
    let expectedRowDiffs = 
        match move.Player with 
        | White -> [1; 2] //todo: only allow row diff of 2 if it's the first move!
        | Black -> [-1; -2]

    let isRowValid = List.contains rowDiff expectedRowDiffs

    (colsMatch && isRowValid)    

let isLegalMove_Knight move source = 
    let (colDiff, rowDiff) = getColAndRowDiff move source

    let absColDiff = Math.Abs(colDiff)
    let absRowDiff = Math.Abs(rowDiff)

    let sum = absColDiff + absRowDiff
    sum = 3

let isLegalMove_Default move source =
    true

let isLegalMove move piece = 
    match piece.PieceType with 
    | Knight -> isLegalMove_Knight move piece.Position
    | Pawn -> isLegalMove_Pawn move piece.Position
    | _ -> isLegalMove_Default move piece.Position

let recordLegalMove move piece = 
    (piece, isLegalMove move piece)

let findLegalPieceMoved possiblePieces move =
    //todo: just return the piece that was moved. don't return bool and then record which pieces were true/false
    let areLegalMoves = List.map (recordLegalMove move) possiblePieces
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
        | _ -> findLegalPieceMoved possiblePieces move

    let viewModel = {
            CellFrom = pieceMoved.Position
            CellTo = move.CellTo
        }
    (viewModel, pieceMoved)    

let rec convertToViewModels moves (board:Map<Player, Map<PieceType, Piece list>>) = 
    match moves with 
    | [] -> []
    | curMove::remainingMoves -> 
        let playerPieces = board.[curMove.Player]
        let (viewModel, pieceMoved) = convertToViewModel moves.Head playerPieces

        //now construct new board, first by creating a new Piece with new Position, then working up the data structures all the way to the board
        let newPiece = { pieceMoved with 
                            Position = viewModel.CellTo}
        let otherPieces = List.filter (fun x -> not (LanguagePrimitives.PhysicalEquality pieceMoved x)) playerPieces.[curMove.PieceTypeMoved]
        let newPieceList = newPiece :: otherPieces
        let newPlayerPieces = playerPieces.Remove(curMove.PieceTypeMoved)
                                .Add(curMove.PieceTypeMoved, newPieceList)
        let newBoard = board.Remove(curMove.Player)
                                .Add(curMove.Player, newPlayerPieces)

        List.append [viewModel] (convertToViewModels remainingMoves newBoard)
