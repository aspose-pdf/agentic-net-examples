using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string reviewer = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF using PdfFileInfo facade, set custom metadata, and save safely
        using (PdfFileInfo pdfInfo = new PdfFileInfo())
        {
            // Bind the existing PDF file
            pdfInfo.BindPdf(inputPath);

            // Add custom metadata field "ReviewedBy"
            pdfInfo.SetMetaInfo("ReviewedBy", reviewer);

            // Persist changes to a new file
            bool success = pdfInfo.SaveNewInfo(outputPath);
            if (!success)
            {
                Console.Error.WriteLine("Failed to save the updated PDF.");
                return;
            }
        }

        Console.WriteLine($"Custom metadata 'ReviewedBy' added and saved to '{outputPath}'.");
    }
}