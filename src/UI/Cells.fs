module Cells

open System

open Browser.Types
open Model

let private clearCell id = 
    let cell:HTMLDivElement = DOM.getDivElementById id
    let innerHtml= cell.innerHTML
    cell.innerHTML <- ""
    innerHtml

let private setCell id innerHtml = 
    let destCell:HTMLDivElement = DOM.getDivElementById id
    destCell.innerHTML <- innerHtml

let private moveDivContents sourceCellId destCellId =
    clearCell sourceCellId
    |> setCell destCellId

let private getColumnIdPart column = 
    match column with 
    | A -> "A"
    | B -> "B"
    | C -> "C"
    | D -> "D"
    | E -> "E"
    | F -> "F"
    | G -> "G"
    | H -> "H"

let private getRowIdPart row = 
    match row with
    | One -> "1"
    | Two -> "2"
    | Three -> "3"
    | Four -> "4"
    | Five -> "5"
    | Six -> "6"
    | Seven -> "7"
    | Eight -> "8"


let private getCoordinateDivId coordinate = 
    let (column, row) = coordinate
    let columnIdPart = getColumnIdPart column
    let rowIdPart = getRowIdPart row
    let coordinateId = String.Format("{0}{1}", columnIdPart, rowIdPart)
    coordinateId

let moveCellContents cellFrom cellTo =
    let fromId = getCoordinateDivId cellFrom
    let toId = getCoordinateDivId cellTo
    moveDivContents fromId toId