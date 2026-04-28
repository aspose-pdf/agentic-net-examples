using System;
using System.IO;
using Aspose.Pdf;

class PdfToDocxAndImageExtractor
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";   // source PDF
        const string outputDocxPath = "output.docx"; // converted DOCX
        const string imagesDir = "ExtractedImages"; // folder for images

        // Verify input file exists
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }

        // Ensure the images output directory exists
        Directory.CreateDirectory(imagesDir);

        // Load the PDF inside a using block (deterministic disposal)
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // ---------- Convert PDF to DOCX ----------
            var docSaveOptions = new DocSaveOptions
            {
                // Export as DOCX (correct enum value – capital X)
                Format = DocSaveOptions.DocFormat.DocX
                // The "Mode" property was removed in recent versions; the default flow
                // recognition mode provides the best editability.
            };

            // Save the document as DOCX – must pass SaveOptions for non‑PDF formats
            pdfDoc.Save(outputDocxPath, docSaveOptions);
            Console.WriteLine($"PDF converted to DOCX: {outputDocxPath}");

            // ---------- Extract embedded images ----------
            int imageCounter = 1; // simple sequential naming

            // Pages are 1‑based in Aspose.Pdf
            for (int pageIndex = 1; pageIndex <= pdfDoc.Pages.Count; pageIndex++)
            {
                Page page = pdfDoc.Pages[pageIndex];

                // XImageCollection is not a dictionary; iterate directly
                foreach (XImage img in page.Resources.Images)
                {
                    // Build a unique file name for each extracted image
                    string imagePath = Path.Combine(
                        imagesDir,
                        $"image_{imageCounter:D4}.png"); // PNG is a safe default

                    // Save the image to the file system via a stream
                    using (FileStream fs = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                    {
                        img.Save(fs);
                    }

                    Console.WriteLine($"Extracted image #{imageCounter} to: {imagePath}");
                    imageCounter++;
                }
            }
        }

        Console.WriteLine("Processing completed.");
    }
}
