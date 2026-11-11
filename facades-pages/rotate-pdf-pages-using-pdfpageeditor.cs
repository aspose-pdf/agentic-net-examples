using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input and output PDF file paths
        const string inputPath  = "input.pdf";
        const string outputPath = "rotated.pdf";

        // Desired rotation angle (must be 0, 90, 180, or 270)
        int rotationAngle = 90; // example value; replace as needed

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Validate the rotation angle against the allowed set
        int[] allowedAngles = { 0, 90, 180, 270 };
        if (Array.IndexOf(allowedAngles, rotationAngle) < 0)
        {
            Console.Error.WriteLine($"Invalid rotation angle: {rotationAngle}. Allowed values are 0, 90, 180, 270.");
            return;
        }

        // Map the integer angle to Aspose.Pdf.Rotation enum
        Rotation rotationEnum = rotationAngle switch
        {
            0   => Rotation.None,   // 0° – no rotation
            90  => Rotation.on90,
            180 => Rotation.on180,
            270 => Rotation.on270,
            _   => Rotation.None   // This line is never reached because of validation
        };

        // Load the PDF document
        Document pdfDocument = new Document(inputPath);
        int pageCount = pdfDocument.Pages.Count; // 1‑based page indexing

        // Apply the rotation to each page
        for (int pageNumber = 1; pageNumber <= pageCount; pageNumber++)
        {
            pdfDocument.Pages[pageNumber].Rotate = rotationEnum;
        }

        // Save the rotated PDF
        pdfDocument.Save(outputPath);
        Console.WriteLine($"Successfully rotated pages by {rotationAngle} degrees and saved to '{outputPath}'.");
    }
}
