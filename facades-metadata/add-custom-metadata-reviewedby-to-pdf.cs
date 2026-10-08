using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string reviewer   = "John Doe";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Bind the PDF file to the PdfFileInfo object
            PdfFileInfo pdfInfo = new PdfFileInfo();
            pdfInfo.BindPdf(inputPath);

            // Set a custom metadata field named "ReviewedBy"
            pdfInfo.SetMetaInfo("ReviewedBy", reviewer);

            // Save the modified PDF to a new file (preserves original)
            pdfInfo.Save(outputPath);

            Console.WriteLine($"Metadata 'ReviewedBy' set to '{reviewer}' and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}