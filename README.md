# FirstFableProject

Install Paket: https://fsprojects.github.io/Paket/get-started.html
Download packages: dotnet paket install

build script must start with these 2 lines:
dotnet tool restore
dotnet paket restore

__________________________________________________
TODO:
  * src/obj/fsac.cache got checked into source control. remove and ignore

__________________________________________________
IMPORTING JS MODULES:

// Same as Import("*", "my-module")
[<ImportAll("my-module")>]
let myModule: obj = jsNative

// Same as Import("default", "my-module")
[<ImportDefault("express")>]
let myModuleDefaultExport: obj = jsNative

// The member name is taken from decorated value, here `myFunction`
[<ImportMember("my-module")>]
let myFunction(x: int): int = jsNative

//If the value is globally accessible in JS, you can use the Global attribute with an optional name parameter instead.
 let [<Global>] console: JS.Console = jsNative