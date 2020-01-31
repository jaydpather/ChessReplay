module AppOld

open Fable.Import

open Model

let mutable moveList: Move list = Logic.getAllMoves ()

let executeNextMove () = 
    let (nextMove, remainingMoves) = Logic.getNextMove moveList
    match nextMove with 
    | Some move -> 
        moveList <- remainingMoves
        Cells.moveCellContents move.CellFrom move.CellTo
    | None -> ()

    

let btnMove_Click e = 
    executeNextMove ()

let btnMove:Browser.Types.HTMLButtonElement = DOM.getButtonElementById "btnMove"
btnMove.onclick <- btnMove_Click

printfn "page loaded"