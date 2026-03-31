using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string metaKey = "ProjectCode";
        const string metaValue = "ABC123";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Bind the existing PDF, set custom metadata, and save the result.
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            pdfInfo.BindPdf(inputPath);
            pdfInfo.SetMetaInfo(metaKey, metaValue);
            bool saved = pdfInfo.SaveNewInfoWithXmp(outputPath);
            Console.WriteLine(saved ? "Custom metadata added successfully." : "Failed to add metadata.");
        }
    }
}
