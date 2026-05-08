using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;               // For FontRepository, HorizontalAlignment, etc.

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF, insert a blank page at position 3, then add a header to every page.
        using (Document doc = new Document(inputPath))
        {
            // Aspose.Pdf uses 1‑based indexing; insert a new empty page at index 3.
            doc.Pages.Insert(3);

            // Create a TextStamp that will act as the header.
            TextStamp headerStamp = new TextStamp("Header Text");
            headerStamp.TextState.Font = FontRepository.FindFont("Helvetica");
            headerStamp.TextState.FontSize = 12;
            headerStamp.TextState.ForegroundColor = Aspose.Pdf.Color.Black;
            headerStamp.HorizontalAlignment = HorizontalAlignment.Center;
            headerStamp.VerticalAlignment = VerticalAlignment.Top;
            headerStamp.YIndent = 20f; // distance from the top edge

            // Apply the stamp to every page.
            foreach (Page page in doc.Pages)
            {
                page.AddStamp(headerStamp);
            }

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with inserted page and header saved to '{outputPath}'.");
    }
}
