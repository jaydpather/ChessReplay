module Model

type Player = Black | White

type Row = One | Two | Three | Four | Five | Six | Seven | Eight

type Column = A | B | C | D | E | F | G | H

type Coordinate = Column * Row

type PieceType = Pawn | Rook | Knight | Bishop | Queen | King

type Piece = {
    PieceType: PieceType;
    Position: Coordinate;
    IsLegalMove: Coordinate -> Coordinate -> bool
}

//todo: move to ViewModel project
type MoveViewModel = {
    CellFrom: Coordinate;
    CellTo: Coordinate;
}

type Move = {
    Player: Player;
    CellTo: Coordinate;
    PieceMoved: Piece;
    PieceCaptured: PieceType option;
}

type ViewState = {
    Moves: (Move * Move) list;
}

type ViewErrorState = {
    ErrorMessage: string;
    InvalidMoves: string list;
}