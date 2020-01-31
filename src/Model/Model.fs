module Model

type Player = Black | White

//todo: should Row and Column be enums? That would make it easier to map to strings
type Row = One | Two | Three | Four | Five | Six | Seven | Eight

type Column = A | B | C | D | E | F | G | H

type Coordinate = Column * Row

type Piece = Pawn | Rook | Knight | Bishop | Queen | King

type Move = {
    Player : Player;
    CellFrom: Coordinate;
    CellTo: Coordinate;
    PieceMoved: Piece;
    PieceCaptured: Piece option;
}
