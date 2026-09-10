using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators; // contains operator types like FillStroke, etc.

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input file exists – create a minimal placeholder if it does not.
        if (!File.Exists(inputPath))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(inputPath);
            }
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (page indexing is 1‑based)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Get the collection of operators in the page's content stream
                OperatorCollection ops = page.Contents;

                // Collect operators that render unwanted background graphics.
                // Example: remove all FillStroke (operator "B") occurrences.
                List<Operator> toDelete = new List<Operator>();
                foreach (Operator op in ops)
                {
                    if (op is FillStroke) // "B" operator – fill and stroke path
                        toDelete.Add(op);
                    // Add other operator type checks here if needed, e.g.:
                    // else if (op is Fill) { ... }
                    // else if (op is Stroke) { ... }
                }

                // Delete the collected operators from the content stream.
                if (toDelete.Count > 0)
                {
                    // Delete(Operator[]) overload
                    ops.Delete(toDelete.ToArray());
                }
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Background operators removed. Output saved to '{outputPath}'.");
    }
}
