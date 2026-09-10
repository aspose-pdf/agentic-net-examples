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

        // Define image rectangle coordinates (lower‑left and upper‑right)
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

        // Bind the PDF, add the PNG to page 2, and save the result
        using (PdfFileMend mender = new PdfFileMend())
        {
            mender.BindPdf(inputPdf);
            mender.AddImage(imagePath, 2, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
            mender.Save(outputPdf);
            mender.Close(); // optional, Dispose will also close
        }

        Console.WriteLine($"Image added to page 2 and saved as '{outputPdf}'.");
    }
}