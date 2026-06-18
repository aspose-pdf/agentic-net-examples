using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sanitized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // ----- Remove embedded JavaScript scripts -----
            if (doc.JavaScript != null)
            {
                // JavaScriptCollection does not implement IEnumerable, iterate via Keys
                var scriptKeys = new List<string>(doc.JavaScript.Keys);
                foreach (var key in scriptKeys)
                {
                    doc.JavaScript.Remove(key);
                }
            }

            // ----- Optionally clear other hidden data such as annotations -----
            foreach (Page page in doc.Pages)
            {
                page.Annotations.Clear();
            }

            // Save the sanitized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}
