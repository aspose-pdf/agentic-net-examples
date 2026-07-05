using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;   // for FontRepository and Color

class AddPageNumbers
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_page_numbers.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a PageNumberStamp – default format is "#"
                PageNumberStamp stamp = new PageNumberStamp();

                // Position the stamp at the bottom centre of the page
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                stamp.BottomMargin        = 20; // distance from the bottom edge

                // Styling of the page number text
                stamp.TextState.Font       = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize   = 12;
                stamp.TextState.ForegroundColor = Color.Black;

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}