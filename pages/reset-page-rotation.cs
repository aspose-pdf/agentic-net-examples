using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "rotated.pdf";
        const string outputPath = "restored.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using block ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; get the page that was previously rotated
            Page page = doc.Pages[1];

            // Display the current rotation value
            Console.WriteLine($"Current rotation: {page.Rotate}");

            // Reset the rotation to its original (no rotation) state
            page.Rotate = Rotation.None;

            // Confirm the rotation has been cleared
            Console.WriteLine($"Rotation after reset: {page.Rotate}");

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}