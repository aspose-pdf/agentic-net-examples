using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators; // operator types such as Do, FillStroke, etc.

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_clean.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];
                OperatorCollection ops = page.Contents;

                // Collect operators that are responsible for drawing background graphics.
                // Example: remove XObject drawing (Do) and path painting operators (FillStroke, ClosePathFillStroke, EOFillStroke, ClosePathEOFillStroke).
                List<Operator> toDelete = new List<Operator>();

                foreach (Operator op in ops)
                {
                    if (op is Do ||
                        op is FillStroke ||
                        op is ClosePathFillStroke ||
                        op is EOFillStroke ||
                        op is ClosePathEOFillStroke)
                    {
                        toDelete.Add(op);
                    }
                }

                // Delete the collected operators in a single call (more efficient than deleting one by one).
                if (toDelete.Count > 0)
                {
                    ops.Delete(toDelete);
                }
            }

            // Save the modified PDF (default Save writes PDF regardless of extension).
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}