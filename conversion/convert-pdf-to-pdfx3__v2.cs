using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfx.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF
        using (Document doc = new Document(inputPath))
        {
            // Set up conversion options for PDF/X-3
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_3)
            {
                // Optional: enable file size optimization
                OptimizeFileSize = true
            };

            // Perform the conversion
            doc.Convert(convOptions);

            // Save the converted PDF/X document to a memory stream
            using (MemoryStream pdfStream = new MemoryStream())
            {
                doc.Save(pdfStream);               // Save as PDF (default format)
                pdfStream.Position = 0;            // Reset stream position for reading

                // Persist the stream contents to a local file
                File.WriteAllBytes(outputPath, pdfStream.ToArray());
                Console.WriteLine($"PDF/X saved to '{outputPath}'.");
            }
        }
    }
}