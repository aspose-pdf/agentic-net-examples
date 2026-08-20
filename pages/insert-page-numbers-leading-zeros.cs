using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_numbered.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a PageNumberStamp with a format that pads single‑digit numbers with a leading zero.
            // The placeholder '#' is replaced by the page number; using "00#" results in "01", "02", … "10", etc.
            PageNumberStamp pageNumberStamp = new PageNumberStamp("00#");
            // Position the stamp at the bottom‑center of each page
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            // Optional: set some margin from the bottom edge
            pageNumberStamp.BottomMargin = 20;
            // Optional: set text appearance (modify the existing TextState instance)
            pageNumberStamp.TextState.FontSize = 12;
            pageNumberStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Apply the stamp to every page in the document
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
    }
}
