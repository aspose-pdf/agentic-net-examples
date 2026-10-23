using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for JavascriptAction

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

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // ----- Document‑level JavaScript actions -----
                Console.WriteLine("Document‑level JavaScript actions:");
                bool docHasJs = false;

                // 1. OpenAction (executed when the document is opened)
                if (doc.OpenAction is JavascriptAction docOpenJs)
                {
                    docHasJs = true;
                    Console.WriteLine("- OpenAction:");
                    Console.WriteLine(docOpenJs.Script);
                    Console.WriteLine();
                }

                // 2. Named JavaScript entries (doc.JavaScript collection)
                if (doc.JavaScript != null && doc.JavaScript.Keys.Count > 0)
                {
                    foreach (string key in doc.JavaScript.Keys)
                    {
                        string script = doc.JavaScript[key];
                        docHasJs = true;
                        Console.WriteLine($"- Named script '{key}':");
                        Console.WriteLine(script);
                        Console.WriteLine();
                    }
                }

                if (!docHasJs)
                {
                    Console.WriteLine("  None.");
                }

                // ----- Page‑level JavaScript actions -----
                foreach (Page page in doc.Pages)
                {
                    bool pageHasJs = false;

                    // OnOpen action
                    if (page.Actions.OnOpen is JavascriptAction onOpenJs)
                    {
                        if (!pageHasJs)
                        {
                            Console.WriteLine($"Page {page.Number} JavaScript actions:");
                            pageHasJs = true;
                        }
                        Console.WriteLine("- OnOpen:");
                        Console.WriteLine(onOpenJs.Script);
                        Console.WriteLine();
                    }

                    // OnClose action
                    if (page.Actions.OnClose is JavascriptAction onCloseJs)
                    {
                        if (!pageHasJs)
                        {
                            Console.WriteLine($"Page {page.Number} JavaScript actions:");
                            pageHasJs = true;
                        }
                        Console.WriteLine("- OnClose:");
                        Console.WriteLine(onCloseJs.Script);
                        Console.WriteLine();
                    }

                    // Uncomment the following line if you want to list pages without scripts
                    // if (!pageHasJs) Console.WriteLine($"Page {page.Number} has no JavaScript actions.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}
