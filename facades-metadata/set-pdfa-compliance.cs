using System;
using System.IO;
using Aspose.Pdf;
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

        // Load the source PDF and convert it to PDF/A‑1b
        using (Document doc = new Document(inputPath))
        {
            doc.Convert("conversion_log.xml", PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);
            doc.Save(outputPath);
        }

        // Use PdfFileInfo to enforce strict validation (PDF/A compliance flag)
        PdfFileInfo fileInfo = new PdfFileInfo(outputPath);
        fileInfo.UseStrictValidation = true;
        // Save the updated information back to the file
        fileInfo.SaveNewInfo(outputPath);

        // Verify that the resulting document is PDF/A compliant
        using (Document checkDoc = new Document(outputPath))
        {
            Console.WriteLine("Is PDF/A compliant: " + checkDoc.IsPdfaCompliant);
        }
    }
}