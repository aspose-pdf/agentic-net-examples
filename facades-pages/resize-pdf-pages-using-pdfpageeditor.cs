using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF as a byte array (could be read from a file, database, etc.)
        const string inputPath  = "input.pdf";
        const string outputPath = "resized_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF bytes into memory
        byte[] pdfBytes = File.ReadAllBytes(inputPath);

        // Use a MemoryStream to bind the PDF to the PdfPageEditor facade
        using (MemoryStream pdfStream = new MemoryStream(pdfBytes))
        using (PdfPageEditor pageEditor = new PdfPageEditor())
        {
            // Bind the PDF document (from the stream) to the editor
            pageEditor.BindPdf(pdfStream);

            // Set the desired output page size.
            // Example: A4 landscape (842 points width, 595 points height)
            pageEditor.PageSize = new PageSize(842F, 595F);

            // Optional: adjust zoom if needed (1.0 = 100%)
            pageEditor.Zoom = 1.0F;

            // Apply the changes (not strictly required before Save, but explicit)
            pageEditor.ApplyChanges();

            // Save the modified PDF to the specified file
            pageEditor.Save(outputPath);
        }

        Console.WriteLine($"Resized PDF saved to '{outputPath}'.");
    }
}