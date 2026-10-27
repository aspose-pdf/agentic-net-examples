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

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
                {
                    Page page = doc.Pages[pageIndex];

                    // Each page may have multiple content streams; in recent versions the
                    // operators are accessed directly via the Contents collection.
                    for (int contentIndex = 1; contentIndex <= page.Contents.Count; contentIndex++)
                    {
                        // If the operator sets the line width, replace it with a width of 3 points.
                        if (page.Contents[contentIndex] is SetLineWidth)
                        {
                            page.Contents[contentIndex] = new SetLineWidth(3);
                        }
                    }
                }

                // Save the modified PDF (default Save writes PDF format)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Line widths updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
