using System;
using System.IO;
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

        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages and apply a stamp with custom border thickness and color (default black)
            for (int pageIndex = 1; pageIndex <= doc.Pages.Count; pageIndex++)
            {
                Page page = doc.Pages[pageIndex];

                // Create a stamp that uses the current page as its content (empty stamp for highlighting)
                PdfPageStamp stamp = new PdfPageStamp(page);
                // Set stamp size (adjust as needed to cover the important section)
                stamp.Width = 200.0f;
                stamp.Height = 100.0f;
                // Position the stamp on the page (example coordinates)
                stamp.XIndent = 100.0f;
                stamp.YIndent = 500.0f;
                // Customize border thickness
                stamp.OutlineWidth = 3.0f;
                // Ensure the border is fully opaque
                stamp.OutlineOpacity = 1.0f;
                // Place the stamp in the foreground (over existing content)
                stamp.Background = false;

                // Add the stamp to the page
                page.AddStamp(stamp);
            }

            doc.Save(outputPath);
        }

        Console.WriteLine($"Stamped PDF saved to '{outputPath}'.");
    }
}