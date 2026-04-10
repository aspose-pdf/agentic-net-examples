using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main(string[] args)
    {
        // Allow the input PDF path to be supplied via command‑line argument; fall back to a default name.
        string inputPath = args.Length > 0 ? args[0] : "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Could not find file '{inputPath}'. Please provide a valid PDF file path as the first argument.");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            Console.WriteLine("=== Document‑level JavaScript actions ===");

            // Document actions (Before/After events)
            DocumentActionCollection docActions = doc.Actions;
            PrintIfJavaScript("AfterPrinting",  docActions.AfterPrinting);
            PrintIfJavaScript("AfterSaving",    docActions.AfterSaving);
            PrintIfJavaScript("BeforeClosing", docActions.BeforeClosing);
            PrintIfJavaScript("BeforePrinting",docActions.BeforePrinting);
            PrintIfJavaScript("BeforeSaving",  docActions.BeforeSaving);

            // Document.JavaScript collection (explicit JS entries)
            var jsCollection = doc.JavaScript;
            if (jsCollection != null && jsCollection.Keys != null && jsCollection.Keys.Count > 0)
            {
                int idx = 1;
                foreach (string key in jsCollection.Keys)
                {
                    string script = jsCollection[key];
                    Console.WriteLine($"Document.JavaScript[{idx}] (key='{key}'): {script}");
                    idx++;
                }
            }

            Console.WriteLine("\n=== Page‑level JavaScript actions ===");
            // Iterate all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Page actions collection (only OnOpen and OnClose are available)
                var pageActions = page.Actions;
                if (pageActions != null)
                {
                    if (pageActions.OnOpen is JavascriptAction jsOnOpen)
                    {
                        Console.WriteLine($"Page {i} OnOpen: {jsOnOpen.Script}");
                    }
                    if (pageActions.OnClose is JavascriptAction jsOnClose)
                    {
                        Console.WriteLine($"Page {i} OnClose: {jsOnClose.Script}");
                    }
                }
            }
        }
    }

    // Helper to output a named document action only when it is JavaScript
    static void PrintIfJavaScript(string name, PdfAction action)
    {
        if (action is JavascriptAction js)
        {
            Console.WriteLine($"{name}: {js.Script}");
        }
        else if (action != null)
        {
            // Non‑JavaScript actions are reported for completeness
            Console.WriteLine($"{name}: (non‑JavaScript action of type {action.GetType().Name})");
        }
    }
}
