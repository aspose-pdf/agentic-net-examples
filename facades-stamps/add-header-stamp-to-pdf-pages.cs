using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;          // TextStamp and related types
using System.Drawing;          // System.Drawing.Color

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string companyName = "Acme Corporation";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (wrapped in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
            for (int pageNumber = 1; pageNumber <= doc.Pages.Count; pageNumber++)
            {
                // Create a TextStamp that will act as the header
                TextStamp headerStamp = new TextStamp(companyName);

                // Configure the visual appearance of the stamp
                headerStamp.TextState.Font = FontRepository.FindFont("Helvetica");
                headerStamp.TextState.FontSize = 12f;
                headerStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
                headerStamp.HorizontalAlignment = HorizontalAlignment.Center;   // Centered horizontally
                headerStamp.VerticalAlignment = VerticalAlignment.Top;          // At the top of the page
                headerStamp.YIndent = 20f;                                      // Slight offset from the top edge

                // Add the stamp to the current page
                doc.Pages[pageNumber].AddStamp(headerStamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Header stamp added to all pages. Output saved to '{outputPath}'.");
    }
}
