using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        int rotationDegrees = 90; // Example rotation value

        bool isValid = (rotationDegrees == 0) || (rotationDegrees == 90) || (rotationDegrees == 180) || (rotationDegrees == 270);
        if (!isValid)
        {
            Console.Error.WriteLine("Invalid rotation value. Allowed values are 0, 90, 180, 270.");
            return;
        }

        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            pageEditor.BindPdf(inputPath);
            pageEditor.Rotation = rotationDegrees;
            pageEditor.Save(outputPath);
        }

        Console.WriteLine("Pages rotated by " + rotationDegrees + " degrees and saved to '" + outputPath + "'.");
    }
}
