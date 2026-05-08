using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string projectCode = "ABC123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, set custom metadata, and save the updated file
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            // Initialize the facade with the source PDF
            pdfInfo.BindPdf(inputPath);

            // Add a custom metadata entry named "ProjectCode"
            pdfInfo.SetMetaInfo("ProjectCode", projectCode);

            // Save the updated PDF to a new file
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            Console.WriteLine(saved
                ? $"Custom metadata added. Saved to '{outputPath}'."
                : "Failed to save the updated PDF.");
        }
    }
}