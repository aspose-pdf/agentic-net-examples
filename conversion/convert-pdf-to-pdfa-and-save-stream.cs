using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_pdfa.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the source PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Configure PDF/A conversion options (PDF/A-1B in this example)
            PdfFormatConversionOptions convOptions = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B)
            {
                // Optional: enable file size optimization
                OptimizeFileSize = true
                // PuaSymbolsProcessingStrategy can be set here if needed
            };

            // Perform the conversion to PDF/A
            doc.Convert(convOptions);

            // Save the converted PDF/A to a memory stream
            using (MemoryStream pdfStream = new MemoryStream())
            {
                doc.Save(pdfStream);               // Save PDF/A content into the stream
                pdfStream.Position = 0;             // Reset position before reading

                // Persist the stream to a local file
                using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    pdfStream.CopyTo(file);
                }

                Console.WriteLine($"PDF/A file saved to '{outputPath}'.");
            }
        }
    }
}