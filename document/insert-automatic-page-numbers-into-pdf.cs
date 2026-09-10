using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text; // Added to resolve FontRepository

class Program
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

        // Load the PDF document (using the required load pattern)
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // Helper: apply page numbers to all pages in the document
            // -----------------------------------------------------------------
            void ApplyPageNumbers()
            {
                // Remove existing page number stamps (if any) to avoid duplication
                foreach (Page p in doc.Pages)
                {
                    // Create a PageNumberStamp with default format ("#")
                    PageNumberStamp stamp = new PageNumberStamp();

                    // Position the stamp at the bottom center of the page
                    stamp.HorizontalAlignment = HorizontalAlignment.Center;
                    stamp.VerticalAlignment   = VerticalAlignment.Bottom;
                    stamp.BottomMargin        = 20; // distance from bottom edge

                    // Optional appearance settings
                    stamp.TextState.FontSize = 12;
                    stamp.TextState.Font      = FontRepository.FindFont("Helvetica");
                    stamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

                    // Add the stamp to the current page
                    p.AddStamp(stamp);
                }
            }

            // Initial stamping of existing pages
            ApplyPageNumbers();

            // -----------------------------------------------------------------
            // Example: insert a new blank page at position 2 (1‑based index)
            // -----------------------------------------------------------------
            doc.Pages.Insert(2); // inserts an empty page after the first page

            // Re‑apply page numbers so the new page also gets a number
            // (stamps are static; they must be added again after page insertion)
            ApplyPageNumbers();

            // Save the modified PDF (using the required save pattern)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with automatic page numbers: {outputPath}");
    }
}
