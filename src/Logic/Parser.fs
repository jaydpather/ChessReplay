module Parser

let parseMoveText moveText = 
    (*We will match the following format:
    (this is a pair of moves by white and black)
        * a digit, dot and space 
        * a group of alphanumeric chars of length 2 or 3
        * a space
        * a group of alphanumeric chars of length 2 or 3
        * a space
    *)
    let regex = "\d. \w{2,3} \w{2,3} "
    System.Text.RegularExpressions.Regex.Matches(moveText, regex)
