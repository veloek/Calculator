module Calculator

open System
open FParsec

let ws = spaces
let braced p = (pchar '(') >>. p .>> (pchar ')')

let add = pchar '+'
let sub = pchar '-'
let mul = pchar '*'
let div = pchar '/'

let operation = (add <|> sub <|> mul <|> div)

let pOperand, opRef = createParserForwardedToRef()

let pExpr = pipe3 pOperand (operation .>> ws) pOperand (fun a op b ->
    match op with
    | '+' -> a + b
    | '-' -> a - b
    | '*' -> a * b
    | '/' -> a / b
    | _ -> 0.0)

do opRef := (braced pExpr <|> pfloat) .>> ws

let parse n =
    match run (pExpr .>> eof) n with
    | Success(result, _, _) -> printfn "%A" result
    | Failure(err, _, _) -> printfn "%s" err
