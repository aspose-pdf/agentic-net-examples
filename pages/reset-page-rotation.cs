using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "rotated.pdf";
        const string outputPath = "original_orientation.pdf";

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

            // Reset the rotation to its original (non‑rotated) state
            page.Rotate = Rotation.None;

            // Save the modified PDF (output is always PDF unless a SaveOptions is provided)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Page rotation reset and saved to '{outputPath}'.");
    }
}