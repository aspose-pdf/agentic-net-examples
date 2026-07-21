using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "rotated.pdf";
        const string outputPath = "restored.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Pages are 1‑based; get the page that was previously rotated
            Page page = doc.Pages[1];

            // Optional: display the current rotation value
            Rotation currentRotation = page.Rotate;
            Console.WriteLine($"Current rotation: {currentRotation}");

            // Reset the page rotation to its original (non‑rotated) state
            page.Rotate = Rotation.None;

            // Verify the rotation has been cleared
            Console.WriteLine($"New rotation: {page.Rotate}");

            // Save the modified PDF (output format is PDF, no SaveOptions needed)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document saved to '{outputPath}'.");
    }
}