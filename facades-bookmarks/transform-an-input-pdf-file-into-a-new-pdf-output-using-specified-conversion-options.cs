using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_converted.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Use PdfConverter (a Facade) to bind the source PDF.
        PdfConverter converter = new PdfConverter();
        try
        {
            // Bind the PDF file to the converter.
            converter.BindPdf(inputPath);

            // Retrieve the underlying Document object.
            Document doc = converter.Document;

            // Define conversion options (e.g., convert to PDF/A-1B and optimize file size).
            PdfFormatConversionOptions options = new PdfFormatConversionOptions(PdfFormat.PDF_A_1B);
            options.OptimizeFileSize = true; // optional optimization flag

            // Perform the conversion using the specified options.
            doc.Convert(options);

            // Save the converted document as a new PDF file.
            doc.Save(outputPath);
            Console.WriteLine($"Conversion completed. Output saved to '{outputPath}'.");
        }
        finally
        {
            // Ensure the converter releases its resources.
            converter.Close();
        }
    }
}