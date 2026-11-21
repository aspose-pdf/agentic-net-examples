using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPdfPath   = "input.pdf";
        const string outputDocxPath = "output.docx";
        const string imagesDir      = "ExtractedImages";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the images output directory exists
        Directory.CreateDirectory(imagesDir);

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // ---------- Convert PDF to DOCX ----------
                // Must use explicit DocSaveOptions; otherwise the file extension is ignored.
                DocSaveOptions docxOptions = new DocSaveOptions
                {
                    Format = DocSaveOptions.DocFormat.DocX
                };
                pdfDoc.Save(outputDocxPath, docxOptions);
                Console.WriteLine($"PDF converted to DOCX: {outputDocxPath}");

                // ---------- Extract all embedded images ----------
                int imageCounter = 1; // simple counter to generate unique file names

                // Pages are 1‑based in Aspose.Pdf
                for (int pageNum = 1; pageNum <= pdfDoc.Pages.Count; pageNum++)
                {
                    Page page = pdfDoc.Pages[pageNum];

                    // Iterate over images in the page resources
                    foreach (XImage img in page.Resources.Images)
                    {
                        // Default to PNG extension – Aspose.Pdf does not expose the original format via XImage
                        string extension = ".png";

                        string imagePath = System.IO.Path.Combine(
                            imagesDir,
                            $"Image_{pageNum}_{imageCounter}{extension}");

                        // Save the image to a file using a FileStream (XImage.Save accepts a Stream)
                        using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                        {
                            img.Save(fs);
                        }

                        Console.WriteLine($"Saved image: {imagePath}");
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
