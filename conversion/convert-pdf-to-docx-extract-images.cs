using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace (Document, Page, XImage, DocSaveOptions, etc.)

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";          // source PDF
        const string outputDocxPath = "output.docx";        // converted DOCX
        const string imagesOutputDir = "ExtractedImages";   // folder for images

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the images output directory exists
        Directory.CreateDirectory(imagesOutputDir);

        try
        {
            // Load the PDF document (using statement ensures proper disposal)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // ---------- Convert PDF to DOCX ----------
                // Configure DOCX save options
                DocSaveOptions docSaveOptions = new DocSaveOptions
                {
                    // Choose DOCX format
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use Flow recognition for better editability (optional)
                    Mode = DocSaveOptions.RecognitionMode.Flow,
                    // Enable bullet recognition (optional)
                    RecognizeBullets = true
                };

                // Save the document as DOCX
                pdfDocument.Save(outputDocxPath, docSaveOptions);
                Console.WriteLine($"PDF converted to DOCX: {outputDocxPath}");

                // ---------- Extract embedded images ----------
                int imageCounter = 1; // global counter for unique filenames

                // Pages are 1‑based in Aspose.Pdf
                for (int pageIndex = 1; pageIndex <= pdfDocument.Pages.Count; pageIndex++)
                {
                    Page page = pdfDocument.Pages[pageIndex];

                    // Iterate over all images defined in the page resources
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Build a unique file name preserving the original image format if possible
                        string extension = Path.GetExtension(img.Name);
                        if (string.IsNullOrEmpty(extension))
                        {
                            // Default to .png when extension is unknown
                            extension = ".png";
                        }

                        string imageFileName = $"image_{pageIndex}_{imageCounter}{extension}";
                        string imagePath = Path.Combine(imagesOutputDir, imageFileName);

                        // XImage.Save overload expects a Stream, so write via FileStream
                        using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs);
                        }

                        Console.WriteLine($"Extracted image: {imagePath}");
                        imageCounter++;
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
