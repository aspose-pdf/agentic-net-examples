using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputPdfPath = "output.pdf";
        const string imagePath     = "image.jpg";
        const int    targetPage    = 25;
        const double posX          = 250;
        const double posY          = 250;

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
            // PdfFileMend does not implement IDisposable, so no using block is needed
            PdfFileMend mend = new PdfFileMend();

            // Load the PDF document to be edited
            mend.BindPdf(inputPdfPath);

            // Add the JPG image to the specified page at (250,250)
            // The AddImage overload expects: (string imagePath, int[] pages, float x, float y, float width, float height)
            // Passing width/height as 0 preserves the original image dimensions.
            mend.AddImage(
                imagePath,
                new int[] { targetPage },
                (float)posX,
                (float)posY,
                0f,
                0f);

            // Save the modified PDF
            mend.Save(outputPdfPath);

            Console.WriteLine($"Image added to page {targetPage} and saved as '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
