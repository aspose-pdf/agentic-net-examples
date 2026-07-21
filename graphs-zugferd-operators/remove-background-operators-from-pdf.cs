using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;   // contains operator types like FillStroke, EOFillStroke, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                // Get the operator collection for the current page
                OperatorCollection ops = doc.Pages[pageNum].Contents;

                // Collect operators that render unwanted background graphics.
                // Example: remove fill/stroke path operators (B, B*, b, b*) which often draw backgrounds.
                List<Operator> toDelete = new List<Operator>();

                foreach (Operator op in ops)
                {
                    if (op is FillStroke ||               // B
                        op is ClosePathFillStroke ||      // B*
                        op is EOFillStroke ||             // B*
                        op is ClosePathEOFillStroke)      // b*
                    {
                        toDelete.Add(op);
                    }
                }

                // Delete the collected operators in one batch (Delete(IList<Operator>) overload)
                if (toDelete.Count > 0)
                {
                    ops.Delete(toDelete);
                }
            }

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}