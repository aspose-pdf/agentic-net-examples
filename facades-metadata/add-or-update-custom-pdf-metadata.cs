using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the PdfFileInfo facade (implements IDisposable)
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Add or update the custom metadata field "Version"
            // Existing custom metadata is left untouched.
            pdfInfo.SetMetaInfo("Version", "1.0");

            // Save the PDF with the updated metadata.
            // SaveNewInfo writes only the changed metadata and preserves all other info.
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved
                ? $"Metadata updated successfully. Output saved to '{outputPath}'."
                : "Failed to save the updated PDF.");
        }
    }
}