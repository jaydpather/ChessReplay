module AppOld

open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Core.Util

[<Emit("alert('$0')")>]
let alert msg = jsNative

let window = Browser.Dom.window
let myParagraph:Browser.Types.HTMLParagraphElement = unbox window.document.getElementById "myParagraph"


myParagraph.className <- "error"

//alert "hello from chess replay"

printfn "page loaded"