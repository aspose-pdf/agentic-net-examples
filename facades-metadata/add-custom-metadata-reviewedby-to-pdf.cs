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

        // Load the PDF using the PdfFileInfo facade
        using (PdfFileInfo pdfInfo = new PdfFileInfo(inputPath))
        {
            // Set the custom metadata field "ReviewedBy"
            pdfInfo.SetMetaInfo("ReviewedBy", reviewer);

            // Persist the changes to a new file
            bool saved = pdfInfo.SaveNewInfo(outputPath);
            if (!saved)
            {
                Console.Error.WriteLine("Failed to save the updated PDF.");
                return;
            }
        }

        Console.WriteLine($"Custom metadata 'ReviewedBy' added and saved to '{outputPath}'.");
    }
}