using System;
using System.Collections.Generic;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators; // Operator types (e.g., FillStroke, FillAndStroke, etc.)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_cleaned.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing).
            for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
            {
                Page page = doc.Pages[pageNum];

                // Get the collection of operators that make up the page's content stream.
                OperatorCollection ops = page.Contents;

                // Collect operators that correspond to background drawing.
                // Typical background graphics use fill (f), fill‑and‑stroke (B), stroke (S), etc.
                // Here we target FillStroke (B) and Fill (f) as examples.
                List<Operator> toDelete = new List<Operator>();

                foreach (Operator op in ops)
                {
                    // Identify operators by their concrete type.
                    if (op is FillStroke)          // corresponds to the 'B' operator
                        toDelete.Add(op);
                    else if (op is Fill)           // corresponds to the 'f' operator
                        toDelete.Add(op);
                    else if (op is Stroke)         // corresponds to the 'S' operator
                        toDelete.Add(op);
                }

                // Delete the collected operators in a single call (more efficient than removing one by one).
                if (toDelete.Count > 0)
                    ops.Delete(toDelete);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
    }
}