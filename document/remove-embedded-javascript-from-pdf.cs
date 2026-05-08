using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "sanitized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // ----- Delete all embedded JavaScript scripts -----
            // The JavaScript collection is not directly iterable, so we iterate over its Keys.
            if (doc.JavaScript != null && doc.JavaScript.Keys.Count > 0)
            {
                // Create a copy of the keys to avoid modifying the collection while iterating.
                var keys = doc.JavaScript.Keys.ToList();
                foreach (var key in keys)
                {
                    // Remove each script by its key.
                    doc.JavaScript.Remove(key);
                }
            }

            // (Optional) If you also want to clear hidden annotations, you can do so:
            // foreach (Page page in doc.Pages)
            // {
            //     page.Annotations.Clear();
            // }

            // Save the sanitized PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Sanitized PDF saved to '{outputPath}'.");
    }
}
