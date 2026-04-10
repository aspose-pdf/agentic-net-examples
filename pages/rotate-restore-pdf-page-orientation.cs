using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string rotatedPath = "rotated.pdf";
        const string restoredPath = "restored.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, rotate the first page, then restore it.
        using (Document doc = new Document(inputPath))
        {
            // Page indexing is 1‑based.
            Page page = doc.Pages[1];

            // Remember the original rotation (usually Rotation.None).
            Rotation originalRotation = page.Rotate;

            // Apply a rotation (example: 90° clockwise).
            page.Rotate = Rotation.on90;
            doc.Save(rotatedPath);
            Console.WriteLine($"Page rotated and saved to '{rotatedPath}'.");

            // Restore the page to its original orientation.
            page.Rotate = originalRotation; // or Rotation.None if you know it was unrotated.
            doc.Save(restoredPath);
            Console.WriteLine($"Page restored and saved to '{restoredPath}'.");
        }
    }
}