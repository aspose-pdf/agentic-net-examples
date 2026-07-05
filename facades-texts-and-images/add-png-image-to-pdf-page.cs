using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string imagePath = "image.png";

        // Coordinates for the image rectangle (lower-left X/Y, upper-right X/Y)
        float lowerLeftX = 100f;
        float lowerLeftY = 200f;
        float upperRightX = 300f;
        float upperRightY = 400f;

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input PDF not found: {inputPdf}");
            return;
        }

        if (!File.Exists(imagePath))
        {
            Console.Error.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the PdfFileMend facade
        PdfFileMend mend = new PdfFileMend();

        // Bind the source PDF document
        mend.BindPdf(inputPdf);

        // Add the PNG image to page 2 at the specified coordinates
        mend.AddImage(imagePath, 2, lowerLeftX, lowerLeftY, upperRightX, upperRightY);

        // Save the modified PDF
        mend.Save(outputPdf);

        // Release resources
        mend.Close();

        Console.WriteLine($"Image added to page 2 and saved as '{outputPdf}'.");
    }
}