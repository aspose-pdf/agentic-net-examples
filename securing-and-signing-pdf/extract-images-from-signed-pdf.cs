using System;
using System.IO;
using Aspose.Pdf;               // Core PDF API
using Aspose.Pdf.Forms;        // For signature fields (if needed)

class Program
{
    static void Main()
    {
        const string inputPdf  = "signed_input.pdf";   // Path to the signed PDF
        const string outputDir = "ExtractedImages";    // Directory to store PNG files

        // Verify input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure output directory exists
        Directory.CreateDirectory(outputDir);

        try
        {
            // Load the PDF document (lifecycle: load)
            using (Document doc = new Document(inputPdf))
            {
                // Iterate through all pages (Aspose.Pdf uses 1‑based indexing)
                for (int pageNum = 1; pageNum <= doc.Pages.Count; pageNum++)
                {
                    Page page = doc.Pages[pageNum];

                    // Each page has a Resources.Images collection (XImageCollection)
                    int imgIndex = 1;
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Build a unique file name per page and image
                        string outPath = Path.Combine(
                            outputDir,
                            $"page{pageNum}_img{imgIndex}.png");

                        // Save the image as PNG using a FileStream (overload expects Stream)
                        using (FileStream fs = new FileStream(outPath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs); // XImage.Save determines format from file extension
                        }

                        Console.WriteLine($"Saved image: {outPath}");
                        imgIndex++;
                    }
                }
            }

            Console.WriteLine("Image extraction completed successfully.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during extraction: {ex.Message}");
        }
    }
}
