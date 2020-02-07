module Coordinate

let toInts coord = 
    let (column, row) = coord
    (Column.toInt(column), Row.toInt(row))