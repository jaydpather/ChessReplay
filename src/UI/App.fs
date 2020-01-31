module AppOld

open Fable.Import

open Model

//let mutable moveList: Move list = []

let executeNextMove () = 
    let nextMove = Logic.getNextMove ()
    Cells.moveCellContents nextMove.CellFrom nextMove.CellTo

let btnMove_Click e = 
    executeNextMove ()

let btnMove:Browser.Types.HTMLButtonElement = DOM.getButtonElementById "btnMove"
btnMove.onclick <- btnMove_Click

printfn "page loaded"