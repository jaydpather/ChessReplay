module CoordCalc

open Model

let convertToViewModel move (playerPieces:Map<PieceType, Piece list>) = 
    let possiblePieces = playerPieces.[move.PieceMoved.PieceType]
    let possibleStartPoints = List.map (fun x -> x.Position) possiblePieces

    let recordLegalMove isLegalMoveFunc startPos  = (startPos, (isLegalMoveFunc startPos))
    let isLegalMoveFuncs = List.map (fun piece -> piece.IsLegalMove piece.Position) possiblePieces

    let recordLegalMoveFuncs = List.map recordLegalMove isLegalMoveFuncs

    let areLegalMoves = List.collect recordLegalMoveFuncs possibleStartPoints 

    let checkIsLegalMove recordedLegalMove =
        let (_, isLegal) = recordedLegalMove
        isLegal

    let legalMoveList = List.filter checkIsLegalMove areLegalMoves
    let onlyLegalMove = legalMoveList.Head

    let (sourceCoord, _) = onlyLegalMove

    {
        CellFrom = sourceCoord
        CellTo = move.CellTo
    }
