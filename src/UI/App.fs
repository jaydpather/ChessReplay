module AppOld

open Fable.Import

open Model

let mutable moveList: MoveViewModel list = Logic.getAllMoves_FromViewModel ()

let executeNextMove () = 
    let (nextMove, remainingMoves) = Logic.getNextMove moveList
    match nextMove with 
    | Some (move:MoveViewModel) -> 
        moveList <- remainingMoves
        Cells.moveCellContents move.CellFrom move.CellTo
    | None -> ()

let btnMove_Click e = 
    executeNextMove ()

let btnMove:Browser.Types.HTMLButtonElement = DOM.getButtonElementById "btnMove"
btnMove.onclick <- btnMove_Click

let allMoves = Logic.getAllMoves_FromViewModel ()

let test = Logic.getAllMoves_FromModel ()

allMoves.ToString()
|> printfn "%s" 


//printfn "page loaded"