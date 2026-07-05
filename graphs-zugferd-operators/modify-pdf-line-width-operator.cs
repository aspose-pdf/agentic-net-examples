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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing for pages)
            foreach (Page page in doc.Pages)
            {
                // The operator collection of a page is accessed via the Contents property
                for (int i = 1; i <= page.Contents.Count; i++)
                {
                    Operator op = page.Contents[i];

                    // Check if the operator sets the line width
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
