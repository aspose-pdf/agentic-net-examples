using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_pdfa1b.pdf";
        const string logPath = "conversion_log.xml"; // optional conversion log

        // Verify that the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // Load the PDF and convert it to PDF/A‑1b. Metadata (Info, XMP, etc.) is preserved automatically.
        using (Document doc = new Document(inputPath))
        {
            // Convert the document in‑place to PDF/A‑1b compliance.
            // ConvertErrorAction.Delete removes objects that prevent compliance.
            doc.Convert(logPath, PdfFormat.PDF_A_1B, ConvertErrorAction.Delete);

            // Save the converted document.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF/A‑1b file created: {outputPath}");
    }
}