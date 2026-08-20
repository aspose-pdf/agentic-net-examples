using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the input PDF exists – create a minimal placeholder if it does not.
        if (!File.Exists(inputPath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(inputPath);
        }

        // Read the PDF into a byte array.
        byte[] pdfBytes = File.ReadAllBytes(inputPath);

        // Load the PDF from the byte array, modify its title metadata, and save.
        using var ms = new MemoryStream(pdfBytes);
        using var doc = new Document(ms);
        // Set new title metadata.
        doc.Info.Title = "Updated Document Title";
        // Save the modified PDF to disk.
        doc.Save(outputPath);

        Console.WriteLine($"PDF saved with updated title to '{outputPath}'.");
    }
}
