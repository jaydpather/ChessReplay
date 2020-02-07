module AppOld

open System
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


// Logic.getAllMoves_FromModel ()
// |> printfn "legal starting points for Knight to A3: %s %A" Environment.NewLine

// let viewState = Logic.getAllMoves_FromText ()
// printfn "loaded moves from text. ViewState: %s %A" Environment.NewLine viewState

printfn "board: %A" BoardFactory.createBoard

//printfn "page loaded"