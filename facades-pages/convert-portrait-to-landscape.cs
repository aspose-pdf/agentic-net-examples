using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "landscape_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // -----------------------------------------------------------------
        // Step 1: Load the document and convert portrait pages to landscape.
        // -----------------------------------------------------------------
        Document doc = new Document(inputPath);

        for (int i = 1; i <= doc.Pages.Count; i++)
        {
            Page page = doc.Pages[i];
            double width = page.PageInfo.Width;
            double height = page.PageInfo.Height;

            // If the page is portrait (height > width) swap dimensions and rotate.
            if (height > width)
            {
                // Swap width and height.
                page.PageInfo.Width = height;
                page.PageInfo.Height = width;

                // Rotate the page content 90 degrees clockwise.
                page.Rotate = Rotation.on90; // Aspose.Pdf.Rotation enum uses the "on" prefix.
            }
        }

        // Save the modified PDF.
        doc.Save(outputPath);

        // -----------------------------------------------------------------
        // Step 2: Verify dimensions after conversion.
        // -----------------------------------------------------------------
        using (Document resultDoc = new Document(outputPath))
        {
            Console.WriteLine($"Verification of '{outputPath}':");
            for (int i = 1; i <= resultDoc.Pages.Count; i++)
            {
                Page page = resultDoc.Pages[i];
                double w = page.PageInfo.Width;
                double h = page.PageInfo.Height;
                Console.WriteLine($"Page {i}: Width = {w:F2}, Height = {h:F2}, Rotation = {page.Rotate}");
            }
        }
    }
}
