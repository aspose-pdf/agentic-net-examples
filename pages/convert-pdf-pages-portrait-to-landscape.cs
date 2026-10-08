using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_landscape.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Pages are 1‑based in Aspose.Pdf
                for (int i = 1; i <= doc.Pages.Count; i++)
                {
                    Page page = doc.Pages[i];

                    // Current MediaBox rectangle
                    Aspose.Pdf.Rectangle mediaBox = page.MediaBox;
                    double width  = mediaBox.URX - mediaBox.LLX;
                    double height = mediaBox.URY - mediaBox.LLY;

                    // Swap width and height to make the page landscape
                    double newURX = mediaBox.LLX + height;
                    double newURY = mediaBox.LLY + width;

                    // Assign the new MediaBox (using fully qualified Rectangle to avoid ambiguity)
                    page.MediaBox = new Aspose.Pdf.Rectangle(mediaBox.LLX, mediaBox.LLY, newURX, newURY);
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"All pages converted to landscape and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}