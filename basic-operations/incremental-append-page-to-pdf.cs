using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_incremental.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Copy the original PDF to the output location.
        File.Copy(inputPath, outputPath, true);

        // Open the copied PDF with a read/write stream.
        using (var fs = new FileStream(outputPath, FileMode.Open, FileAccess.ReadWrite))
        using (var doc = new Document(fs))
        {
            // Append a new blank page.
            doc.Pages.Add();

            // Parameterless Save performs an incremental update.
            doc.Save();
        }

        Console.WriteLine($"PDF saved with incremental update to '{outputPath}'.");
    }
}
