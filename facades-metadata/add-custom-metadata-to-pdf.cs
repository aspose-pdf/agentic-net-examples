using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string projectCode = "ABC123";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file information (metadata handling) via Facades API
        PdfFileInfo pdfInfo = new PdfFileInfo(inputPath);

        // Add or update a custom metadata entry named "ProjectCode"
        pdfInfo.SetMetaInfo("ProjectCode", projectCode);

        // Save the PDF with the updated metadata to a new file
        pdfInfo.Save(outputPath);

        Console.WriteLine($"Custom metadata 'ProjectCode' added and saved to '{outputPath}'.");
    }
}