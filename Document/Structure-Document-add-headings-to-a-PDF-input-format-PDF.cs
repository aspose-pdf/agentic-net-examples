using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main(string[] args)
    {
        // Input and output PDF paths (adjust as needed)
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_headings.pdf";

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // -----------------------------------------------------------------
            // Add a first‑level heading to the first page
            // -----------------------------------------------------------------
            // Create a Heading object (level 1)
            Heading heading1 = new Heading(1)
            {
                Text = "Chapter 1 – Introduction",
                Level = 1,
                // Position the heading near the top of the page (Y coordinate in points)
                Position = new Position(0, 750), // XIndent = 0 (centered), YIndent = 750
                HorizontalAlignment = HorizontalAlignment.Center
            };

            // Add the heading to the page's paragraph collection
            pdfDocument.Pages[1].Paragraphs.Add(heading1);

            // -----------------------------------------------------------------
            // Add a second‑level heading to the second page
            // -----------------------------------------------------------------
            if (pdfDocument.Pages.Count >= 2)
            {
                Heading heading2 = new Heading(2)
                {
                    Text = "1.1 Background",
                    Level = 2,
                    Position = new Position(0, 700),
                    HorizontalAlignment = HorizontalAlignment.Left
                };

                pdfDocument.Pages[2].Paragraphs.Add(heading2);
            }

            // -----------------------------------------------------------------
            // Save the modified PDF (uses the provided document-save rule)
            // -----------------------------------------------------------------
            pdfDocument.Save(outputPath);
            Console.WriteLine($"PDF saved successfully to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}