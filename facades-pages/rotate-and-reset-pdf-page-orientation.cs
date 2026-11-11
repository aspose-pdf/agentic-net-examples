using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";
        const string rotatedPath = "rotated.pdf";
        const string resetPath   = "reset.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // -------------------------------------------------
        // Rotate the first page by 90 degrees and save it
        // -------------------------------------------------
        Document doc = new Document(inputPath);          // Load the source PDF
        doc.Pages[1].Rotate = Rotation.on90;             // Rotate page 1 (90° clockwise)
        doc.Save(rotatedPath);                           // Save the rotated PDF
        Console.WriteLine($"Page rotated and saved to '{rotatedPath}'.");

        // -------------------------------------------------
        // Reset the rotation back to 0 degrees (original orientation)
        // -------------------------------------------------
        Document rotatedDoc = new Document(rotatedPath); // Load the rotated PDF
        rotatedDoc.Pages[1].Rotate = Rotation.None;      // Reset rotation to 0°
        rotatedDoc.Save(resetPath);                      // Save the restored PDF
        Console.WriteLine($"Rotation reset and saved to '{resetPath}'.");
    }
}
