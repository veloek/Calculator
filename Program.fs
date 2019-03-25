open System
open Calculator

[<EntryPoint>]
let main argv =
    let input = match argv.Length with
                | 0 -> Seq.initInfinite (fun _ -> Console.In.ReadLine())
                    |> Seq.takeWhile (fun line -> line <> null)
                | _ -> argv |> (String.concat "") |> Seq.singleton

    input |> Seq.iter Calculator.parse
    0
