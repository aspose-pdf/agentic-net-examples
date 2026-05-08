using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Pages to delete: 5 through 10 (inclusive)
            int[] pagesToDelete = new int[] { 5, 6, 7, 8, 9, 10 };

            // Single Delete operation using the int[] overload
            doc.Pages.Delete(pagesToDelete);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Pages 5‑10 removed. Saved to '{outputPath}'.");
    }
}