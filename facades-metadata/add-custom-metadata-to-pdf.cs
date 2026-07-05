using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using the PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Add a custom metadata entry named "ProjectCode"
            pdfInfo.SetMetaInfo("ProjectCode", "ABC123");

            // Save the updated PDF to a new file
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            if (!saved)
            {
                Console.Error.WriteLine("Failed to save the updated PDF.");
            }
            else
            {
                Console.WriteLine($"Custom metadata added and saved to '{outputPath}'.");
            }
        }
    }
}