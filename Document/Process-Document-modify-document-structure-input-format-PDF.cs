using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "modified.pdf";
        const string imagePath     = "stamp.png";

        // Verify that the required files exist
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdfPath}");
            return;
        }
        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDocument = new Document(inputPdfPath))
        {
            // PdfFileMend is a facade that allows adding images (or other content) to existing pages
            // It does NOT rely on System.Drawing, so it works cross‑platform.
            PdfFileMend mend = new PdfFileMend(pdfDocument);

            // Add the image to page 1.
            // Parameters: image file path, page number, lower‑left X, lower‑left Y, upper‑right X, upper‑right Y
            // Coordinates are in points (1/72 inch). Adjust as needed.
            mend.AddImage(imagePath, 1, 100f, 500f, 300f, 700f);

            // Save the modified PDF. No additional SaveOptions are required for PDF output.
            pdfDocument.Save(outputPdfPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdfPath}'.");
    }
}