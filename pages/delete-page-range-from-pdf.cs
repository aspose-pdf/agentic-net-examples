using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Delete pages 5 through 10 in a single operation using Delete(int[])
            int[] pagesToDelete = { 5, 6, 7, 8, 9, 10 };
            doc.Pages.Delete(pagesToDelete);

            // Save the modified document
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages 5-10 removed. Result saved to '{outputPath}'.");
    }
}