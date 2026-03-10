using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "formatted_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle: using ensures proper disposal)
        using (Document pdfDoc = new Document(inputPath))
        {
            // Example formatting: set all pages to A4 size and add header/footer text
            foreach (Page page in pdfDoc.Pages)
            {
                // Set page size to A4 (595 x 842 points)
                page.SetPageSize(595, 842);

                // Add a header at the top of the page
                TextFragment header = new TextFragment("Document Header")
                {
                    // Use a cross‑platform color
                    TextState = { FontSize = 12, ForegroundColor = Color.Gray }
                };
                // Position header (centered at top)
                header.Position = new Position(0, page.PageInfo.Height - 20);
                header.HorizontalAlignment = HorizontalAlignment.Center;
                page.Paragraphs.Add(header);

                // Add a footer at the bottom of the page
                TextFragment footer = new TextFragment($"Page {page.Number} of {pdfDoc.Pages.Count}")
                {
                    TextState = { FontSize = 10, ForegroundColor = Color.Gray }
                };
                // Position footer (centered at bottom)
                footer.Position = new Position(0, 20);
                footer.HorizontalAlignment = HorizontalAlignment.Center;
                page.Paragraphs.Add(footer);
            }

            // Save the formatted PDF (lifecycle: Save inside using block)
            pdfDoc.Save(outputPath);
        }

        Console.WriteLine($"Formatted PDF saved to '{outputPath}'.");
    }
}