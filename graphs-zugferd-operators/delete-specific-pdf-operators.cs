using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Access the operator collection of the first page
            OperatorCollection ops = doc.Pages[1].Contents;

            // Delete a specific operator by its 1‑based index (e.g., the 3rd operator)
            if (ops.Count >= 3)
                ops.Delete(3);

            // Optionally delete multiple operators at once:
            // Operator[] toRemove = new Operator[] { ops[1], ops[2] };
            // ops.Delete(toRemove);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}