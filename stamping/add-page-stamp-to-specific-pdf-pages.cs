using System;
using System.IO;
using Aspose.Pdf;

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

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a stamp using the first page of the document as the stamp source
            // (any page can be used; here we use page 1)
            PdfPageStamp stamp = new PdfPageStamp(doc.Pages[1])
            {
                // Example settings – stamp will appear behind page content
                Background = true,
                Opacity   = 0.5f
            };

            // Apply the stamp to pages 5 through 10 (inclusive)
            // Ensure we do not exceed the actual page count
            int lastPage = Math.Min(10, doc.Pages.Count);
            for (int i = 5; i <= lastPage; i++)
            {
                // Each target page receives the same stamp instance
                doc.Pages[i].AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}