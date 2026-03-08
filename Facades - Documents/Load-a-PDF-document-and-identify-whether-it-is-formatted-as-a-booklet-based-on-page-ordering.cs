using System;
using System.IO;
using System.Collections; // Added for StructuralComparisons
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string tempPath   = "temp_booklet.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create the facade (PdfFileEditor does NOT implement IDisposable)
        PdfFileEditor editor = new PdfFileEditor();

        // Generate a booklet version of the PDF and save it to a temporary file
        bool made = editor.MakeBooklet(inputPath, tempPath);
        if (!made)
        {
            Console.Error.WriteLine("Failed to create booklet version.");
            return;
        }

        // Read both files into memory for comparison
        byte[] originalBytes = File.ReadAllBytes(inputPath);
        byte[] bookletBytes  = File.ReadAllBytes(tempPath);

        // If the byte content is identical, the original PDF is already a booklet
        bool isBooklet = originalBytes.Length == bookletBytes.Length &&
                         StructuralComparisons.StructuralEqualityComparer.Equals(originalBytes, bookletBytes);

        Console.WriteLine(isBooklet
            ? "The PDF is already formatted as a booklet."
            : "The PDF is NOT formatted as a booklet.");

        // Clean up the temporary file
        try { File.Delete(tempPath); } catch { /* ignore cleanup errors */ }
    }
}
