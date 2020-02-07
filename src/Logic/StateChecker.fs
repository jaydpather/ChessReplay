module StateChecker

let checkState nextFunc state = 
    match state with 
    | Ok o -> nextFunc o
    | Error e -> Error e