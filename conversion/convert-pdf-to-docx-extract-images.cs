using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";                 // source PDF
        const string outputDocxPath = "output.docx";                // converted DOCX
        const string imagesOutputDir = "ExtractedImages";           // folder for images

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the images output directory exists
        Directory.CreateDirectory(imagesOutputDir);

        try
        {
            // Load the PDF document (lifecycle rule: use Document constructor)
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // ---------- Convert PDF to DOCX ----------
                // Configure DOCX save options
                DocSaveOptions docxOptions = new DocSaveOptions
                {
                    // Use DOCX format
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Choose Flow mode for better editability
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Optional: improve bullet detection
                    RecognizeBullets = true
                };

                // Save as DOCX (lifecycle rule: use Document.Save with SaveOptions)
                pdfDoc.Save(outputDocxPath, docxOptions);
                Console.WriteLine($"PDF converted to DOCX: {outputDocxPath}");

                // ---------- Extract embedded images ----------
                int imageCounter = 1;

                // Iterate through all pages (1‑based indexing rule)
                for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
                {
                    Page page = pdfDoc.Pages[pageIndex];

                    // Iterate over the XImage collection (foreach, not dictionary)
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Aspose.Pdf's XImage does not expose image format directly in all versions.
                        // For simplicity, we save every extracted image as PNG. If needed, you can
                        // inspect the raw image bytes to determine the format.
                        string extension = ".png";

                        // Build a unique file name
                        string imageFileName = $"img_{pageIndex}_{imageCounter}{extension}";
                        string imagePath = Path.Combine(imagesOutputDir, imageFileName);

                        // Save the image to disk using a FileStream (XImage.Save overload expects a Stream)
                        using (var fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs);
                        }
                        Console.WriteLine($"Extracted image: {imagePath}");

                        imageCounter++;
                    }
                }

                Console.WriteLine($"All images extracted to: {imagesOutputDir}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
