using System;
using System.IO;
using Aspose.Pdf.Facades;   // PdfFileInfo resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_metadata.pdf";
        const string metaKey    = "ProjectCode";
        const string metaValue  = "ABC-1234";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF file using PdfFileInfo (Facades API)
            PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);

            // Set a custom metadata entry
            pdfInfo.SetMetaInfo(metaKey, metaValue);

            // Save the modified PDF to a new file
            pdfInfo.Save(outputPath);

            Console.WriteLine($"Metadata \"{metaKey}\" added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}