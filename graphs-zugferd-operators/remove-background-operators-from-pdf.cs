using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators; // contains operator classes like Do

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Wrap Document in a using block for deterministic disposal (rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Iterate pages using 1‑based indexing (rule: page-indexing-one-based)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                // OperatorCollection is also 1‑based (rule: operator-collection-1based)
                var ops = page.Contents;

                // Delete unwanted operators (e.g., Do – XObject painting) while iterating backwards
                // (rule: delete-operators-iterate-backwards)
                for (int i = ops.Count; i >= 1; i--)
                {
                    if (ops[i] is Do) // correct class name, not DoOperator
                    {
                        ops.Delete(i); // use Delete, not RemoveAt
                    }
                }
            }

            // Save the modified PDF (no special SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background graphics removed. Saved to '{outputPath}'.");
    }
}
