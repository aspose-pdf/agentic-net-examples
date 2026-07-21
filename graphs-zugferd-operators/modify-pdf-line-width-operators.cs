using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // The operator collection is accessed via the Contents property (1‑based)
                for (int i = 1; i <= page.Contents.Count; i++)
                {
                    Operator op = page.Contents[i];
                    // Check if the operator sets line width
                    if (op is SetLineWidth setLineWidth)
                    {
                        // Change width from 1 point to 3 points
                        if (Math.Abs(setLineWidth.Width - 1.0) < 0.0001)
                        {
                            setLineWidth.Width = 3.0;
                        }
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Line widths updated and saved to '{outputPath}'.");
    }
}
