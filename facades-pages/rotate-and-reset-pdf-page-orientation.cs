using System;
using System.IO;
using Aspose.Pdf; // Document API

class Program
{
    static void Main()
    {
        const string inputPath   = "input.pdf";
        const string rotatedPath = "rotated.pdf";
        const string resetPath   = "reset.pdf";
        const int pageNumber     = 1; // first page (1‑based indexing)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // ---------- Rotate the page ----------
        // Load the source PDF, rotate the specified page, and save the result.
        Document doc = new Document(inputPath);
        // Rotation enum values: None, on90, on180, on270
        doc.Pages[pageNumber].Rotate = Rotation.on90; // rotate 90° clockwise
        doc.Save(rotatedPath);
        Console.WriteLine($"Page {pageNumber} rotated and saved to '{rotatedPath}'.");

        // ---------- Reset the rotation ----------
        // Load the rotated PDF, reset the rotation, and save the final document.
        Document resetDoc = new Document(rotatedPath);
        resetDoc.Pages[pageNumber].Rotate = Rotation.None; // back to original orientation
        resetDoc.Save(resetPath);
        Console.WriteLine($"Page {pageNumber} rotation reset and saved to '{resetPath}'.");
    }
}