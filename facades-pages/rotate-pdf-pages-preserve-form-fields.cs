using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated.pdf";
        // 90° clockwise rotation
        const Rotation rotateAngle = Rotation.on90;

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document. Form fields are preserved automatically.
        Document pdfDocument = new Document(inputPath);

        // Rotate each page individually while keeping the interactive form fields intact.
        for (int pageNum = 1; pageNum <= pdfDocument.Pages.Count; pageNum++)
        {
            pdfDocument.Pages[pageNum].Rotate = rotateAngle;
        }

        // Save the rotated PDF.
        pdfDocument.Save(outputPath);

        Console.WriteLine($"Rotated PDF saved to '{outputPath}'.");
    }
}
