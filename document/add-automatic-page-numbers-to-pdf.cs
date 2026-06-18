using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // needed for FontRepository

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

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a page number stamp with default format "#"
            Aspose.Pdf.PageNumberStamp pageNumberStamp = new Aspose.Pdf.PageNumberStamp();

            // Configure appearance of the stamp
            pageNumberStamp.HorizontalAlignment = HorizontalAlignment.Center;
            pageNumberStamp.VerticalAlignment   = VerticalAlignment.Bottom;
            pageNumberStamp.BottomMargin        = 20; // distance from bottom edge
            pageNumberStamp.TextState.FontSize  = 12;
            pageNumberStamp.TextState.Font      = FontRepository.FindFont("Helvetica");
            pageNumberStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;

            // Apply the stamp to every existing page
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(pageNumberStamp);
            }

            // Example: insert a new blank page at position 2 (pages are 1‑based)
            doc.Pages.Insert(2);

            // After inserting pages, update pagination so numbers reflect the new layout
            doc.Pages.UpdatePagination();

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document with automatic page numbers saved to '{outputPath}'.");
    }
}