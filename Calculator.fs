module Calculator

open FParsec

let ws = spaces
let braced p = (pchar '(') >>. p .>> (pchar ')')

let add = pchar '+' |>> (fun _ a b -> a + b)
let sub = pchar '-' |>> (fun _ a b -> a - b)
let mul = pchar '*' |>> (fun _ a b -> a * b)
let div = pchar '/' |>> (fun _ a b -> a / b)

let pOperator = (add <|> sub <|> mul <|> div)
let pOperand, opRef = createParserForwardedToRef()
let pExpr = pipe2 pOperand (many ((pOperator .>> ws) .>>. pOperand)) (fun start ops ->
    ops |> List.fold (fun a (op, b) -> op a b) start)

do opRef := (braced pExpr <|> pfloat) .>> ws

let parse n =
    match run (pExpr .>> eof) n with
    | Success(result, _, _) -> printfn "%A" result
    | Failure(err, _, _) -> printfn "%s" err
