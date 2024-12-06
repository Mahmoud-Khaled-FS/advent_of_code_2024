open System

let calcPart1 (rules: Map<int, int list>, updates: int list) =
    let mutable isOk = true

    let rec loopUpdates (updates: int list) =
        if updates.Length > 1 then
            for i = updates.Length - 2 downto 0 do
                let ruleIndex = updates.Length - 1

                if rules.ContainsKey updates[ruleIndex] then
                    if rules.[updates[ruleIndex]] |> List.contains updates[i] then
                        isOk <- false

            loopUpdates updates[0 .. updates.Length - 2]

    loopUpdates updates
    isOk

let rec orderList (rules: Map<int, int list>, updates: int list) =
    let mutable updatesList = updates
    let isOk = calcPart1 (rules, updatesList)

    if isOk then
        updates
    else
        // System.Environment.Exit 1
        let mutable outLoop = true
        let mutable i = updatesList.Length - 1

        while outLoop do
            let mutable j = i - 1
            let mutable inLoop = true

            while inLoop do

                if rules.ContainsKey updatesList[i] then
                    if rules.[updatesList[i]] |> List.contains updatesList[j] then
                        let temp = updatesList[i]

                        updatesList <-
                            List.mapi
                                (fun index v ->
                                    if index = i then updatesList[j]
                                    elif index = j then updatesList[i]
                                    else v)
                                updatesList

                        j <- 0
                        i <- 0

                if j = 0 then
                    inLoop <- false

                j <- j - 1


            if i = 0 then
                outLoop <- false

            i <- i - 1

        orderList (rules, updatesList)

let solveDay5 (content: string array) =
    let mutable rulesOrder = Map.empty<int, int list>
    let mutable i = 0

    while not (String.IsNullOrWhiteSpace content[i]) do
        let line = content[i]
        let numbers = line.Split "|"
        let key = numbers[0] |> int
        let value = numbers[1] |> int

        let updateMap (l: int list option) =
            match l with
            | Some list -> Some(value :: list)
            | None -> None

        if rulesOrder.ContainsKey key then
            rulesOrder <- rulesOrder |> Map.change key updateMap
        else
            rulesOrder <- rulesOrder |> Map.add key [ value ]

        i <- i + 1

    i <- i + 1
    let mutable resultPart1 = 0
    let mutable resultPart2 = 0

    while i < content.Length do
        let line = content[i]
        let list = line.Split "," |> List.ofArray
        let numberList: int list = List.map (fun x -> x |> int) list
        let isOk = calcPart1 (rulesOrder, numberList)

        if isOk then
            let index = numberList.Length / 2 |> float |> Math.Floor |> int
            resultPart1 <- resultPart1 + numberList[index]
        else
            let orderUpdate = orderList (rulesOrder, numberList)
            let index = orderUpdate.Length / 2 |> float |> Math.Floor |> int
            resultPart2 <- resultPart2 + orderUpdate[index]

        i <- i + 1

    (resultPart1, resultPart2)

[<EntryPoint>]
let main (args) =
    if args.Length < 1 then
        printfn "ERROR: No file path"
        System.Environment.Exit 1

    let (result1, result2) = System.IO.File.ReadAllLines(args[0]) |> solveDay5
    printfn "Result Part 1 -> %d" result1
    printfn "Result Part 2 -> %d" result2
    0
