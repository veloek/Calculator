open System
open Calculator

[<EntryPoint>]
let main argv =
    Calculator.parse (argv |> String.concat "")
    0
