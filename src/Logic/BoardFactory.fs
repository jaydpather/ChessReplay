module BoardFactory

open System

open Model

let private createPieces pieceType coords = 
    List.map (PieceFactory.createPiece pieceType) coords

let createBoard =
    //board will be map of Player -> (map of PieceType -> Piece list)

    let whitePiecesMap = 
        Map.empty
            .Add(Pawn, (createPieces Pawn [(A,Two); (B,Two); (C,Two); (D,Two); (E,Two); (F,Two); (G,Two); (H,Two);]))
            .Add(Rook, (createPieces Rook [(A,One); (H,One)]))
            .Add(Knight, (createPieces Knight [(B,One); (G,One)]))
            .Add(Bishop, (createPieces Bishop [(C,One); (F,One)]))
            .Add(Queen, (createPieces Queen [(D,One)]))
            .Add(King, (createPieces King [(E,One)]))

    let blackPiecesMap = 
        Map.empty
            .Add(Pawn, (createPieces Pawn [(A,Seven); (B,Seven); (C,Seven); (D,Seven); (E,Seven); (F,Seven); (G,Seven); (H,Seven);]))        
            .Add(Rook, (createPieces Rook [(A,Eight); (H,Eight)]))
            .Add(Knight, (createPieces Knight [(B,Eight); (G,Eight)]))
            .Add(Bishop, (createPieces Bishop [(C,Eight); (F,Eight)]))
            .Add(Queen, (createPieces Queen [(D,Eight)]))
            .Add(King, (createPieces King [(E,Eight)]))


    Map.empty
        .Add(White, whitePiecesMap)
        .Add(Black, blackPiecesMap)

//board factory must call PieceFactory
//PieceFactory needs to know position
//board factory must have logic to determine position of each piece