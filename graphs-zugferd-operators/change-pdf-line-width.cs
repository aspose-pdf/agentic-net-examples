using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages
            foreach (Page page in doc.Pages)
            {
                // The operator collection is accessed via the Contents property (1‑based index)
                for (int i = 1; i <= page.Contents.Count; i++)
                {
                    Operator op = page.Contents[i];
                    // Check if the operator sets line width and its current width is 1 point
                    if (op is SetLineWidth setLineWidth && Math.Abs(setLineWidth.Width - 1.0) < 0.0001)
                    {
                        // Change the line width to 3 points
                        setLineWidth.Width = 3.0;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
