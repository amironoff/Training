open System

// interface with a single method that maps a string to a boolean
type ILogger =
    abstract member LogMessage: string -> bool

// an actual implementation
type ConsoleLogger() = 
    interface ILogger with
        member this.LogMessage message =
            Console.WriteLine message |> ignore
            true

let square x = x * x
let isOdd x = x % 2 <> 0
//let getSquaresOfOdds nums = 
//    nums |> List.filter isOdd |> List.map square
let getSquaresOfOdds nums = 
    nums |> List.filter isOdd |> List.map (fun x -> (x, square x))

[<EntryPoint>]
let main argv =
    printfn "Hello world! Square of %d is %d" 12 (square 12)

    printfn "Testing the logger"
    let logger = new ConsoleLogger() :> ILogger
    logger.LogMessage "Here's a nice message straight from the logger..." |> ignore
    let numbers = [1 .. 25]
    let squareOfOdds = getSquaresOfOdds numbers

    for (num, square) in squareOfOdds do
        printfn "%d squared is %d" num square

    // F# requires you to explicitly ignore values
    // returned in a statement.
    Console.Read() |> ignore
    0


//open System
//open System.Text.RegularExpressions
//
//let square x = x * x
//let isOdd x = x % 2 <> 0
//
//let getSquaresOfOdds nums =
//    nums |> List.filter isOdd |> List.map (fun x -> (x, square x))
//
//[<EntryPoint>]
//let main argv = 
//    printfn "Hello, world!  Square of %d is %d" 12 (square 12)
//
//    let numers = [ 1 .. 25 ]
//    let squaresOfOdds = getSquaresOfOdds numers
//
//    for (num, square) in squaresOfOdds do
//        printfn "%d squared is %d" num square
//
//    let message = "Seth is super awesome and he is fun to talk to"
//
//    let superCount = Regex.Matches(message, "super").Count
//
//    printfn "%d" superCount
//
//    Console.Read() |> ignore
//    0 // return an interger exit code