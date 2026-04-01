using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string imagePath = "image.png";
        const string outputPdfPath = "output.pdf";

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
            using (Document pdfDocument = new Document(inputPdfPath))
            {
                PdfFileMend mender = new PdfFileMend();
                mender.BindPdf(pdfDocument);

                // Add PNG image to page 2 at the specified rectangle (lower‑left and upper‑right coordinates)
                float lowerLeftX = 100f;
                float lowerLeftY = 200f;
                float upperRightX = 300f;
                float upperRightY = 400f;
                mender.AddImage(imagePath, 2, lowerLeftX, lowerLeftY, upperRightX, upperRightY);

                mender.Save(outputPdfPath);
                mender.Close();
            }

            Console.WriteLine($"Image added and saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}