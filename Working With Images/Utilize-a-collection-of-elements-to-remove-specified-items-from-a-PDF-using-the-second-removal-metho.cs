using System;
using System.IO;
using System.Collections.Generic;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for proper disposal
            using (Document doc = new Document(inputPath))
            {
                // Access the operator collection of the first page (1‑based indexing)
                OperatorCollection ops = doc.Pages[1].Contents;

                // Build a list of operators to delete (example: first two operators if they exist)
                List<Operator> toDelete = new List<Operator>();
                if (ops.Count >= 1) toDelete.Add(ops[1]); // operators are 1‑based
                if (ops.Count >= 2) toDelete.Add(ops[2]);

                // Use the IList<Operator> overload to remove the selected operators
                ops.Delete(toDelete);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}