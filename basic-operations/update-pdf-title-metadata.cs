using System;
using System.IO;
using Aspose.Pdf;

#pragma warning disable NU1902, NU1903 // Suppress known vulnerability warnings for System.Security.Cryptography.Pkcs

class Program
{
    static void Main()
    {
        const string sourcePath = "input.pdf";
        const string outputPath = "output.pdf";

        // Ensure the source PDF exists; create a minimal placeholder if it does not.
        if (!File.Exists(sourcePath))
        {
            using var placeholder = new Document();
            placeholder.Pages.Add();
            placeholder.Save(sourcePath);
        }

        // Read the PDF file into a byte array.
        byte[] pdfBytes = File.ReadAllBytes(sourcePath);

        // Load the PDF from the byte array using a MemoryStream.
        using var ms = new MemoryStream(pdfBytes);
        ms.Position = 0; // Ensure the stream is at the beginning.
        using var doc = new Document(ms);

        // Modify the document's title metadata.
        doc.Info.Title = "Updated Document Title";

        // Save the modified PDF to disk.
        doc.Save(outputPath);

        Console.WriteLine($"PDF saved with updated title to '{outputPath}'.");
    }
}
