using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputPdfPath = "output_pdfx3.pdf";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        try
        {
            // Load the source PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdfPath))
            {
                // Set up conversion options for PDF/X-3
                PdfFormatConversionOptions conversionOptions = new PdfFormatConversionOptions(PdfFormat.PDF_X_3)
                {
                    // Optional: enable file size optimization
                    OptimizeFileSize = true
                    // Note: PuaSymbolsProcessingStrategy can be set here if needed
                };

                // Perform the conversion to PDF/X-3
                doc.Convert(conversionOptions);

                // Save the converted PDF to a memory stream
                using (MemoryStream pdfStream = new MemoryStream())
                {
                    doc.Save(pdfStream);

                    // Ensure the stream position is at the beginning before writing to file
                    pdfStream.Position = 0;

                    // Persist the stream contents to a local file
                    using (FileStream file = new FileStream(outputPdfPath, FileMode.Create, FileAccess.Write))
                    {
                        pdfStream.CopyTo(file);
                    }
                }
            }

            Console.WriteLine($"PDF/X-3 file saved to '{outputPdfPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}