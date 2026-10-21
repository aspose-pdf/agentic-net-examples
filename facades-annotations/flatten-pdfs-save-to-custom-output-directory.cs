using System;
using System.IO;
using Aspose.Pdf; // Document API provides flattening support

class Program
{
    static void Main()
    {
        // Input PDF to be flattened
        const string inputPdf = "input.pdf";

        // Custom output directory for flattened PDFs
        const string outputDir = "FlattenedOutputs";

        // Validate input file
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDir);

        // Build the output file path (preserve original name with a suffix)
        string outputPdf = Path.Combine(outputDir,
            Path.GetFileNameWithoutExtension(inputPdf) + "_flattened.pdf");

        try
        {
            // Load the PDF using Document (Aspose.Pdf) – this class supports flattening.
            Document pdfDoc = new Document(inputPdf);

            // Flatten the PDF: removes interactive elements (annotations, form fields)
            pdfDoc.Flatten();

            // Save the flattened PDF to the custom output directory.
            pdfDoc.Save(outputPdf);

            Console.WriteLine($"Flattened PDF saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during flattening: {ex.Message}");
        }
    }
}
