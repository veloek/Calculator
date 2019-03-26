module Calculator

open FParsec

let ws = spaces
let braced p = (pchar '(') >>. p .>> (pchar ')')

let operator op _ = op

let pAdd = pchar '+' |>> operator (+)
let pSub = pchar '-' |>> operator (-)
let pMul = pchar '*' |>> operator (*)
let pDiv = pchar '/' |>> operator (/)
let pMod = pchar '%' |>> operator (%)

let pOperator = (pAdd <|> pSub <|> pMul <|> pDiv <|> pMod)
let pOperand, opRef = createParserForwardedToRef()
let pExpr = pipe2 pOperand (many ((pOperator .>> ws) .>>. pOperand)) (fun start ops ->
    ops |> List.fold (fun a (op, b) -> op a b) start)

do opRef := (braced pExpr <|> pfloat) .>> ws

let parse n =
    match run (pExpr .>> eof) n with
    | Success(result, _, _) -> printfn "%A" result
    | Failure(err, _, _) -> printfn "%s" err
