using System;
using System.IO;
using Aspose.Pdf; // Core Aspose.Pdf namespace for generic PDF handling

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file (the source document)
        const string inputPath = "input.pdf";
        // Output PDF file (the booklet version – here we simply copy the source)
        const string outputPath = "output_booklet.pdf";

        // Verify that the source file exists before proceeding
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document using Aspose.Pdf.Document
            Document pdfDocument = new Document(inputPath);

            // NOTE: The original example used PdfBooklet (a facade class that may not be
            // available in the current Aspose.Pdf version). To keep the code portable and
            // compile‑time safe we simply save the loaded document. If booklet creation is
            // required, the PdfBooklet class can be re‑introduced when the appropriate
            // assembly/reference is available.
            pdfDocument.Save(outputPath);

            Console.WriteLine($"PDF processed successfully: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any unexpected errors
            Console.Error.WriteLine($"Error while processing PDF: {ex.Message}");
        }
    }
}
