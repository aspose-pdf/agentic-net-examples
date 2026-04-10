using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text; // Added for FontRepository

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "numbered_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate through all pages (1‑based indexing)
            for (int i = 1; i <= doc.Pages.Count; i++)
            {
                // Create a PageNumberStamp with a format that pads single‑digit numbers with a leading zero
                // The format string "00" forces two digits (01, 02, …)
                PageNumberStamp stamp = new PageNumberStamp("00")
                {
                    // Position the stamp at the bottom‑center of the page
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment   = VerticalAlignment.Bottom,
                    // Optional styling
                    TextState = {
                        FontSize = 12,
                        Font = FontRepository.FindFont("Helvetica"),
                        ForegroundColor = Color.Gray
                    }
                };

                // Add the stamp to the current page
                doc.Pages[i].AddStamp(stamp);
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page numbers with leading zeros saved to '{outputPath}'.");
    }
}
