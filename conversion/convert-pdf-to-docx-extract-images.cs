using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputDocxPath = "output.docx";
        const string imagesOutputDir = "ExtractedImages";

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the images directory exists
        Directory.CreateDirectory(imagesOutputDir);

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // ---------- Convert PDF to DOCX ----------
                // Must pass a DocSaveOptions instance; otherwise the file is saved as PDF regardless of extension
                DocSaveOptions docOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX
                };
                pdfDoc.Save(outputDocxPath, docOptions);
                Console.WriteLine($"PDF successfully converted to DOCX: {outputDocxPath}");

                // ---------- Extract embedded images ----------
                int imageIndex = 1; // simple counter to create unique filenames

                // Aspose.Pdf uses 1‑based page indexing
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];

                    // XImageCollection is not a dictionary; iterate directly
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Aspose.Pdf's XImage does not expose an ImageFormat property.
                        // Default to PNG which works for the majority of extracted images.
                        const string ext = "png";

                        string imageFileName = $"image_page{pageNum}_{imageIndex}.{ext}";
                        string imagePath = Path.Combine(imagesOutputDir, imageFileName);

                        // Save the image to the designated file
                        using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs);
                        }

                        Console.WriteLine($"Extracted image saved to: {imagePath}");
                        imageIndex++;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
