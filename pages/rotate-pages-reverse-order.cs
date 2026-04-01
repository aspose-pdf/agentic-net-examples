using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        string inputPath = "input.pdf";
        string outputPath = "output.pdf";

        // Ensure the source PDF exists – create a simple one if it does not.
        if (!File.Exists(inputPath))
        {
            CreateSamplePdf(inputPath);
        }

        using (Document pdfDocument = new Document(inputPath))
        {
            int pageCount = pdfDocument.Pages.Count;

            // Rotate pages in reverse order (last page first).
            for (int pageIndex = pageCount; pageIndex >= 1; pageIndex--)
            {
                Page page = pdfDocument.Pages[pageIndex];
                page.Rotate = Rotation.on90; // 90 degrees clockwise
            }

            pdfDocument.Save(outputPath);
        }

        Console.WriteLine($"Pages rotated and saved to {outputPath}");
    }

    // Helper method to generate a minimal PDF with a few pages.
    private static void CreateSamplePdf(string path)
    {
        using (Document doc = new Document())
        {
            // Add three blank pages as a simple example.
            for (int i = 0; i < 3; i++)
            {
                doc.Pages.Add();
            }
            doc.Save(path);
        }
    }
}
