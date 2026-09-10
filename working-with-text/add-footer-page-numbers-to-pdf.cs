using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // for Stamp base class
using Aspose.Pdf.Text;   // for TextFragment if needed

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_footer.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (Aspose.Pdf uses 1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                Page page = doc.Pages[i];

                // Create a page‑number stamp. The default format "#" will be replaced
                // with the actual page number when the stamp is applied.
                PageNumberStamp pageNumberStamp = new PageNumberStamp();

                // Position the stamp at the bottom centre of the page
                pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
                pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;

                // Optional: adjust margins or font size
                pageNumberStamp.BottomMargin = 20; // 20 points from the bottom edge
                pageNumberStamp.TextState.FontSize = 10;
                pageNumberStamp.TextState.Font = FontRepository.FindFont("Helvetica");
                pageNumberStamp.TextState.ForegroundColor = Color.Gray;

                // Add the stamp to the current page
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Footer with page numbers added: {outputPath}");
    }
}