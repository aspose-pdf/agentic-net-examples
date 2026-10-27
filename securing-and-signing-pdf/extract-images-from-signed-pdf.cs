using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "signed_input.pdf";
        const string outputFolder = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load the PDF inside a using block (document-disposal-with-using rule)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Iterate pages using 1‑based indexing (page-indexing-one-based rule)
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];
                    int imageIndex = 1;

                    // XImageCollection is not a dictionary; iterate directly (ximage-collection-foreach-only-no-keys rule)
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Build a unique file name per page and image
                        string fileName = $"page_{pageNum}_img_{imageIndex}.png";
                        string outputPath = Path.Combine(outputFolder, fileName);

                        // Save the image using a FileStream (use-stream-with-ximage-save rule)
                        using (FileStream fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs);
                        }

                        Console.WriteLine($"Saved image: {outputPath}");
                        imageIndex++;
                    }
                }
            }

            Console.WriteLine("Image extraction completed.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
