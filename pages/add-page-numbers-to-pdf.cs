using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDocument = new Document(inputPath))
            {
                // Iterate using 1‑based page indexing
                for (int i = 1; i <= pdfDocument.Pages.Count; i++)
                {
                    // Create a text stamp containing the page number
                    TextStamp pageNumberStamp = new TextStamp(i.ToString())
                    {
                        // Center the stamp horizontally and vertically
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment   = VerticalAlignment.Center,
                        // Optional appearance settings
                        TextState = { FontSize = 12, ForegroundColor = Color.Black }
                    };

                    // Add the stamp to the current page (per‑page call, not on the collection)
                    pdfDocument.Pages[i].AddStamp(pageNumberStamp);
                }

                // Save the modified PDF
                pdfDocument.Save(outputPath);
            }

            Console.WriteLine($"Page numbers added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}