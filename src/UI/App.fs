module AppOld

open System
open Fable.Import

open Model

let board = BoardFactory.createBoard
//printfn "board: %A" board

let result = Logic.getAllMoves_FromText board

let moveListToUse = 
    match result with 
    | Ok viewState -> viewState.Moves
    | Error e ->
        printfn "invalid move text: %A" e 
        []

let mutable moveList: MoveViewModel list = moveListToUse

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


//printfn "page loaded"