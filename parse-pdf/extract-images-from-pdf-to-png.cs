using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";               // Path to source PDF
        const string outputFolder = "ExtractedImages";         // Folder for PNG files

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputFolder);

        try
        {
            // Load PDF document (using block ensures proper disposal)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                int imageIndex = 0;

                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];

                    // Iterate over all images defined in the page resources
                    foreach (XImage xImg in page.Resources.Images)
                    {
                        imageIndex++;
                        string outPath = Path.Combine(outputFolder, $"image_{imageIndex}.png");

                        // Save the image using a FileStream because XImage.Save expects a Stream.
                        using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                        {
                            xImg.Save(fs);
                        }

                        Console.WriteLine($"Saved image {imageIndex} → {outPath}");
                    }
                }

                Console.WriteLine($"Extraction complete. {imageIndex} image(s) saved to '{outputFolder}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
