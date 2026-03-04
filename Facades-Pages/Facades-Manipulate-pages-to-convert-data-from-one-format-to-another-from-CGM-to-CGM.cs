using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input CGM file and desired PDF output file
        const string inputCgmPath = "input.cgm";
        const string outputPdfPath = "output.pdf";

        // Verify that the source CGM file exists
        if (!File.Exists(inputCgmPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputCgmPath}");
            return;
        }

        try
        {
            // Create the output PDF file stream inside a using block for deterministic disposal
            using (FileStream pdfStream = File.Create(outputPdfPath))
            {
                // Use the PdfProducer facade to convert the CGM file to PDF.
                // CGM is an input‑only format; it cannot be saved as CGM.
                // The Produce method takes the input file name, the import format,
                // and the destination stream where the PDF will be written.
                PdfProducer.Produce(inputCgmPath, ImportFormat.Cgm, pdfStream);
            }

            Console.WriteLine($"Successfully converted CGM to PDF: {outputPdfPath}");
        }
        // PdfException is the base exception type for Aspose.Pdf conversion errors
        catch (PdfException ex)
        {
            Console.Error.WriteLine($"Conversion failed: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}