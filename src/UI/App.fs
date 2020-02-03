module AppOld

open Fable.Import

open Model

let mutable moveList: MoveViewModel list = Logic.getAllMoves_Old ()

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

let allMoves = Logic.getAllMoves ()

allMoves.ToString()
|> printfn "%s" 


//printfn "page loaded"