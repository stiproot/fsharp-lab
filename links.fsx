#r "nuget:FSharp.Data"

open FSharp.Data
open System.IO

let getHtml (htmlFile: string) : HtmlDocument option =
    try
        let html = HtmlDocument.Load(htmlFile)
        Some(html)
    with
    | ex ->
        printfn $"Error: {ex}"
        None

let htmlPath = Path.Join(__SOURCE_DIRECTORY__,"fsharp-github-repo.html")

getHtml htmlPath

(** PIPELINES **)

let getLinks (html: HtmlDocument option) =
    // Pattern match over option type to get 'a' html tags in document
    match html with
    | Some (x) -> x.Descendants [ "a" ]
    | None -> Seq.empty

// Call getHtml and getLinks
getLinks (getHtml htmlPath)

// Use pipe operator |> to call getHtml and getLinks
htmlPath |> getHtml |> getLinks

(** COMPOSITION **)

// Use composition operator >> to 
// combine getHtml and getLinks into one function
let getLinksFromHtml = getHtml >> getLinks

// Call getLinksFromHtml
getLinksFromHtml htmlPath

// Use pipe operator and lambda expression for additional processing
htmlPath
|> getLinksFromHtml 
|> fun links -> printfn $"{links}"