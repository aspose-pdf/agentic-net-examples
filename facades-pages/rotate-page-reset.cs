using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string rotatedPath = "rotated.pdf";
        const string resetPath = "reset.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Store the original rotation of the first page
            Rotation originalRotation = doc.Pages[1].Rotate;

            // Apply a 90‑degree rotation to the first page using the correct enum value
            doc.Pages[1].Rotate = Rotation.on90;

            // Save the rotated version
            doc.Save(rotatedPath);
            Console.WriteLine($"Rotated page saved to '{rotatedPath}'.");

            // Reset the page back to its original rotation
            doc.Pages[1].Rotate = originalRotation;

            // Save the reset version
            doc.Save(resetPath);
            Console.WriteLine($"Reset page saved to '{resetPath}'.");
        }
    }
}