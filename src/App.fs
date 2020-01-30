module AppOld

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Core.Util

open Browser.Types

[<Emit("alert('$0')")>]
let alert msg = jsNative

let _window = Browser.Dom.window

let btnMove_Click e = 
    let sourceCell:HTMLDivElement = unbox _window.document.getElementById "A2"
    let destCell:HTMLDivElement = unbox _window.document.getElementById "A3"

    let innerHtml= sourceCell.innerHTML
    sourceCell.innerHTML <- ""
    destCell.innerHTML <- innerHtml

let btnMove:Browser.Types.HTMLButtonElement = unbox _window.document.getElementById "btnMove"

btnMove.onclick <- btnMove_Click

printfn "page loaded"