using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";               // source PDF
        const string outputDocxPath = "output.docx";              // converted DOCX
        const string imagesDir      = "ExtractedImages";         // folder for images

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the images output directory exists
        Directory.CreateDirectory(imagesDir);

        try
        {
            // Load the PDF document (using the standard Document constructor)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // ---------- Convert PDF to DOCX ----------
                // DocSaveOptions controls the DOC/DOCX conversion.
                DocSaveOptions docOptions = new DocSaveOptions
                {
                    // Specify DOCX output format
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use Flow mode for better editability (optional)
                    Mode = DocSaveOptions.RecognitionMode.Flow
                };

                // Save the document as DOCX using the options
                pdfDocument.Save(outputDocxPath, docOptions);
                Console.WriteLine($"PDF converted to DOCX: {outputDocxPath}");

                // ---------- Extract embedded images ----------
                int imageIndex = 0;

                // Iterate over all pages (1‑based indexing)
                for (int pageNum = 1; pageNum <= pdfDocument.Pages.Count; pageNum++)
                {
                    Page page = pdfDocument.Pages[pageNum];

                    // XImageCollection is enumerable; iterate directly
                    foreach (XImage img in page.Resources.Images)
                    {
                        imageIndex++;
                        // Build a unique file name for each image
                        string imagePath = Path.Combine(imagesDir, $"image_{imageIndex}.png");

                        // Save the image using a FileStream because XImage.Save expects a Stream
                        using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs);
                        }

                        Console.WriteLine($"Extracted image {imageIndex}: {imagePath}");
                    }
                }

                Console.WriteLine($"Total images extracted: {imageIndex}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
