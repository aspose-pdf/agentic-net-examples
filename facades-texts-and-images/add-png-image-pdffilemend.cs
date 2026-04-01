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

        PdfFileMend mend = new PdfFileMend();
        mend.BindPdf(inputPdf);

        // Define image rectangle coordinates (lower‑left and upper‑right)
        float lowerLeftX = 100f;
        float lowerLeftY = 200f;
        float upperRightX = 300f;
        float upperRightY = 400f;

        bool success = mend.AddImage(imagePath, 2, lowerLeftX, lowerLeftY, upperRightX, upperRightY);
        if (!success)
        {
            Console.Error.WriteLine("Failed to add image to the PDF.");
        }

        mend.Save(outputPdf);
        mend.Close();

        Console.WriteLine($"Image added to page 2 and saved as '{outputPdf}'.");
    }
}