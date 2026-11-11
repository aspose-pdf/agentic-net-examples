using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_resized.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the original PDF, read the rotation of the first page (1‑based index)
        int originalRotation;
        const float newWidth = 595f;   // A4 width in points
        const float newHeight = 842f;  // A4 height in points

        using (Document srcDoc = new Document(inputPath))
        {
            Page srcPage = srcDoc.Pages[1];
            // Page.Rotate returns a Rotation enum; cast to int to get degrees (0, 90, 180, 270)
            originalRotation = (int)srcPage.Rotate;
            Console.WriteLine($"Original rotation: {originalRotation} degrees");

            // Change the page size while keeping the rotation unchanged
            srcPage.SetPageSize(newWidth, newHeight);
            srcDoc.Save(outputPath);
        }
        Console.WriteLine($"Page size changed and saved to {outputPath}");

        // Load the modified PDF and verify that the rotation is unchanged
        using (Document outDoc = new Document(outputPath))
        {
            Page outPage = outDoc.Pages[1];
            int newRotation = (int)outPage.Rotate;
            Console.WriteLine($"Rotation after size change: {newRotation} degrees");

            if (newRotation == originalRotation)
                Console.WriteLine("Rotation value remained unchanged.");
            else
                Console.WriteLine("Rotation value changed!");
        }
    }
}
