using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // FontRepository, Color

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // ------------------------------------------------------------
        // Ensure the source PDF exists. If it does not, create a simple
        // one‑page document so the example can run without external files.
        // ------------------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (Document placeholder = new Document())
            {
                // Add a single blank page (you can add more if you wish)
                placeholder.Pages.Add();
                placeholder.Save(inputPath);
            }
        }

        // Load the PDF document (using ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all pages (1‑based indexing)
            foreach (Page page in doc.Pages)
            {
                // Create a page number stamp; default format is "#"
                PageNumberStamp stamp = new PageNumberStamp();

                // Position the stamp in the footer, centered horizontally
                stamp.BottomMargin        = 20; // distance from bottom edge
                stamp.HorizontalAlignment = HorizontalAlignment.Center;
                stamp.VerticalAlignment   = VerticalAlignment.Bottom;

                // Set visual appearance (font, size, color)
                stamp.TextState.Font          = FontRepository.FindFont("Helvetica");
                stamp.TextState.FontSize      = 12;
                stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                // Add the stamp to the current page
                page.AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}
