using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";   // source PDF
        const string imagePath     = "image.jpg";   // image to embed
        const string outputPdfPath = "output.pdf";  // result PDF

        // Verify required files exist
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

        try
        {
            // PdfFileMend does not implement IDisposable, so no using block
            PdfFileMend mend = new PdfFileMend();

            // Load the PDF document into the editor
            mend.BindPdf(inputPdfPath);

            // Add the image on page 27 within rectangle (100,100)-(400,400)
            // Coordinates are in points; lower‑left (llx,lly) and upper‑right (urx,ury)
            mend.AddImage(imagePath, 27, 100, 100, 400, 400);

            // Save the modified PDF
            mend.Save(outputPdfPath);

            Console.WriteLine($"Image added and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}