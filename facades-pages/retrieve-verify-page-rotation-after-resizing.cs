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

        // Load the original PDF and read its rotation
        int rotationBefore;
        using (Document doc = new Document(inputPath))
        {
            // Rotation is exposed via the Page.Rotate property (enum Rotation)
            rotationBefore = (int)doc.Pages[1].Rotate;
            Console.WriteLine($"Rotation before resizing: {rotationBefore} degrees");

            // Change the page size – A4 size is 595 x 842 points
            // SetPageSize expects width and height as double values, not a PageSize object.
            doc.Pages[1].SetPageSize(595.0, 842.0);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        // Load the modified PDF and verify that rotation is unchanged
        int rotationAfter;
        using (Document doc = new Document(outputPath))
        {
            rotationAfter = (int)doc.Pages[1].Rotate;
            Console.WriteLine($"Rotation after resizing: {rotationAfter} degrees");
        }

        // Simple check
        if (rotationBefore == rotationAfter)
            Console.WriteLine("Rotation value remained unchanged after resizing.");
        else
            Console.WriteLine("Rotation value changed after resizing.");
    }
}
