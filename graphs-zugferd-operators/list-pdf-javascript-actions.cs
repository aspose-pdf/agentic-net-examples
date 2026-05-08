using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // JavascriptAction, PdfAction

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // ------------------------------------------------------------
            // Document‑level JavaScript actions
            // ------------------------------------------------------------
            Console.WriteLine("Document‑level JavaScript actions:");
            if (doc.JavaScript != null && doc.JavaScript.Keys.Count > 0)
            {
                // JavaScriptCollection is not enumerable; iterate via its Keys collection
                foreach (string key in doc.JavaScript.Keys)
                {
                    string script = doc.JavaScript[key];
                    Console.WriteLine($"- Name: {key}, Script: {script}");
                }
            }
            else
            {
                Console.WriteLine("(none)");
            }

            // ------------------------------------------------------------
            // Page‑level JavaScript actions (OnOpen / OnClose)
            // ------------------------------------------------------------
            foreach (Page page in doc.Pages)
            {
                if (page.Actions != null)
                {
                    // OnOpen action
                    if (page.Actions.OnOpen is JavascriptAction onOpenJs)
                    {
                        Console.WriteLine($"Page {page.Number} OnOpen JavaScript: {onOpenJs.Script}");
                    }

                    // OnClose action
                    if (page.Actions.OnClose is JavascriptAction onCloseJs)
                    {
                        Console.WriteLine($"Page {page.Number} OnClose JavaScript: {onCloseJs.Script}");
                    }
                }
            }
        }
    }
}
