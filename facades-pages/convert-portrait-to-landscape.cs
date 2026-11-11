using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "portrait.pdf";
        const string outputPath = "landscape.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document
        Document pdf = new Document(inputPath);

        // Work with the first page (1‑based index)
        int pageNumber = 1;
        Page page = pdf.Pages[pageNumber];

        // Original dimensions
        double originalWidth = page.PageInfo.Width;
        double originalHeight = page.PageInfo.Height;
        Console.WriteLine($"Original size – Width: {originalWidth}, Height: {originalHeight}");

        // Swap width and height to obtain landscape orientation
        page.PageInfo.Width = originalHeight;
        page.PageInfo.Height = originalWidth;
        page.PageInfo.IsLandscape = true; // optional flag indicating landscape

        // Verify new dimensions
        double newWidth = page.PageInfo.Width;
        double newHeight = page.PageInfo.Height;
        Console.WriteLine($"New size – Width: {newWidth}, Height: {newHeight}");

        // Optional visual rotation (90°) – rotates the page content
        page.Rotate = Rotation.on90;

        // Save the modified PDF
        pdf.Save(outputPath);
        Console.WriteLine($"Landscape PDF saved to '{outputPath}'.");
    }
}
