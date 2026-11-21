using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            Console.WriteLine("=== Document‑level JavaScript actions ===");

            // 1. OpenAction (executed when the document is opened)
            if (doc.OpenAction is JavascriptAction openJs)
            {
                Console.WriteLine("[Document] OpenAction script:");
                Console.WriteLine(openJs.Script);
                Console.WriteLine(new string('-', 40));
            }

            // 2. Named JavaScript entries stored in the JavaScript collection.
            //    The collection is not enumerable, so we iterate via its Keys property.
            if (doc.JavaScript != null && doc.JavaScript.Keys.Count > 0)
            {
                foreach (string key in doc.JavaScript.Keys)
                {
                    string script = doc.JavaScript[key];
                    Console.WriteLine($"[Document] Named script '{key}':");
                    Console.WriteLine(script);
                    Console.WriteLine(new string('-', 40));
                }
            }

            Console.WriteLine("=== Page‑level JavaScript actions ===");
            // Pages are 1‑based in Aspose.Pdf.
            foreach (Page page in doc.Pages)
            {
                // OnOpen action
                if (page.Actions.OnOpen is JavascriptAction onOpenJs)
                {
                    Console.WriteLine($"[Page {page.Number}] OnOpen script:");
                    Console.WriteLine(onOpenJs.Script);
                    Console.WriteLine(new string('-', 40));
                }

                // OnClose action
                if (page.Actions.OnClose is JavascriptAction onCloseJs)
                {
                    Console.WriteLine($"[Page {page.Number}] OnClose script:");
                    Console.WriteLine(onCloseJs.Script);
                    Console.WriteLine(new string('-', 40));
                }
            }
        }
    }
}
