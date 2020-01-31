module AppOld

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Core.Util
open Browser.Types

open System

open Model

[<Emit("alert('$0')")>]
let alert msg = jsNative

let window = Browser.Dom.window

let getElementByIdAbstract (window:Window) id = 
    unbox window.document.getElementById id

let getDivElementById = 
    getElementByIdAbstract window

let getButtonElementById = 
    getElementByIdAbstract window

let clearCell id = 
    let cell:HTMLDivElement = getDivElementById id
    let innerHtml= cell.innerHTML
    cell.innerHTML <- ""
    innerHtml

let setCell id innerHtml = 
    let destCell:HTMLDivElement = getDivElementById id
    destCell.innerHTML <- innerHtml

let moveDivContents sourceCellId destCellId =
    clearCell sourceCellId
    |> setCell destCellId

let getColumnIdPart column = 
    match column with 
    | A -> "A"
    | B -> "B"
    | C -> "C"
    | D -> "D"
    | E -> "E"
    | F -> "F"
    | G -> "G"
    | H -> "H"

let getRowIdPart row = 
    match row with
    | One -> "1"
    | Two -> "2"
    | Three -> "3"
    | Four -> "4"
    | Five -> "5"
    | Six -> "6"
    | Seven -> "7"
    | Eight -> "8"


let getCoordinateDivId coordinate = 
    let (column, row) = coordinate
    let columnIdPart = getColumnIdPart column
    let rowIdPart = getRowIdPart row
    let coordinateId = String.Format("{0}{1}", columnIdPart, rowIdPart)
    coordinateId

let executeNextMove () = 
    let nextMove = Logic.getNextMove ()
    let fromId = getCoordinateDivId nextMove.CellFrom
    let toId = getCoordinateDivId nextMove.CellTo
    moveDivContents fromId toId

let btnMove_Click e = 
    executeNextMove ()

let btnMove:Browser.Types.HTMLButtonElement = getButtonElementById "btnMove"
btnMove.onclick <- btnMove_Click

printfn "page loaded"