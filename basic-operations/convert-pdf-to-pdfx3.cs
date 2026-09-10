using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfx3.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the source PDF document
        using (Document doc = new Document(inputPath))
        {
            // Convert the document to PDF/X‑3 compliance.
            // The Convert method modifies the document in‑place and preserves
            // any embedded ICC color profiles automatically.
            doc.Convert("conversion_log.xml", PdfFormat.PDF_X_3, ConvertErrorAction.Delete);

            // Save the converted document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF successfully saved as PDF/X‑3 to '{outputPath}'.");
    }
}
