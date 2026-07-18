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
            // Load the PDF document (wrapped in using for deterministic disposal)
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                // ---------- Convert PDF to DOCX ----------
                // DocSaveOptions must be passed explicitly for non‑PDF output
                DocSaveOptions docOptions = new DocSaveOptions
                {
                    // Specify DOCX output format
                    Format = DocSaveOptions.DocFormat.DocX,
                    // Use Flow mode for better editability
                    Mode = DocSaveOptions.RecognitionMode.Flow
                };
                pdfDocument.Save(outputDocxPath, docOptions);

                // ---------- Extract embedded images ----------
                int imageCounter = 1;
                foreach (Page page in pdfDocument.Pages)
                {
                    // XImageCollection is iterated directly (no dictionary semantics)
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Save each image as PNG using a FileStream (XImage.Save expects a Stream)
                        string imageFileName = $"image_page{page.Number}_{imageCounter}.png";
                        string imagePath = Path.Combine(imagesOutputDir, imageFileName);

                        using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs);
                        }

                        imageCounter++;
                    }
                }
            }

            Console.WriteLine("PDF successfully converted to DOCX and images extracted.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
