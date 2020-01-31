module DOM

open Browser.Types
open Fable.Core
open Fable.Core.JsInterop
open Fable.Import
open Fable.Core.Util


[<Emit("alert('$0')")>]
let alert msg = jsNative

let private window = Browser.Dom.window

let private getElementByIdAbstract (window:Window) id = 
    unbox window.document.getElementById id

let getDivElementById (id:string) = 
    getElementByIdAbstract window id

let getButtonElementById (id:string) = 
    getElementByIdAbstract window id