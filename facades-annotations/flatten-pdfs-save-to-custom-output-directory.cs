using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input PDF file to be flattened
        const string inputPdfPath = "input.pdf";

        // Custom output directory for flattened PDFs
        const string outputDirectory = "Flattened";

        // Validate input file
        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Ensure the output directory exists
        Directory.CreateDirectory(outputDirectory);

        // Build the output file path (e.g., input_flattened.pdf)
        string outputPdfPath = Path.Combine(
            outputDirectory,
            Path.GetFileNameWithoutExtension(inputPdfPath) + "_flattened.pdf");

        try
        {
            // Load the PDF document using Document class
            Document pdfDoc = new Document(inputPdfPath);

            // Flatten all form fields – they become static content
            pdfDoc.Form.Flatten();

            // Save the flattened PDF to the custom directory
            pdfDoc.Save(outputPdfPath);

            Console.WriteLine($"Flattened PDF saved to: {outputPdfPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during flattening: {ex.Message}");
        }
    }
}
