using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string referencePath = "template.pdf";
        const string inputPath = "input.pdf";
        const string outputPath = "resized.pdf";

        if (!File.Exists(referencePath))
        {
            Console.Error.WriteLine($"Reference file not found: {referencePath}");
            return;
        }
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the reference PDF to obtain the desired page size (first page)
            using (Document referenceDoc = new Document(referencePath))
            {
                if (referenceDoc.Pages.Count == 0)
                {
                    Console.Error.WriteLine("Reference PDF contains no pages.");
                    return;
                }

                double targetWidth = referenceDoc.Pages[1].PageInfo.Width;
                double targetHeight = referenceDoc.Pages[1].PageInfo.Height;

                // Load the PDF whose pages will be resized
                using (Document sourceDoc = new Document(inputPath))
                {
                    // Iterate using 1‑based indexing (Aspose.Pdf requirement)
                    for (int i = 1; i <= sourceDoc.Pages.Count; i++)
                    {
                        Page page = sourceDoc.Pages[i];
                        // Resize each page to match the reference dimensions
                        page.SetPageSize(targetWidth, targetHeight);
                    }

                    // Save the modified document
                    sourceDoc.Save(outputPath);
                }
            }

            Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}