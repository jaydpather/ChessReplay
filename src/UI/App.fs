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

let allMoves = Logic.getAllMoves_FromViewModel ()
allMoves.ToString()
|> printfn "all moves (from view model): %s %s" Environment.NewLine

Logic.getAllMoves_FromModel ()
|> printfn "legal starting points for Knight to A3: %s %A" Environment.NewLine

let movesFromText:System.Text.RegularExpressions.MatchCollection = Logic.getAllMoves_FromText ()
movesFromText.ToString()
|> printfn "all moves (from text): %s %s" Environment.NewLine 



//printfn "page loaded"