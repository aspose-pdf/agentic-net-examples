using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades; // Facades namespace as required

class Program
{
    static void Main()
    {
        // Input PDF to be flattened
        const string inputPdf = "input.pdf";

        // Custom output directory for flattened PDFs
        const string outputDirectory = "FlattenedOutput";

        // Ensure the input file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create the output directory if it does not exist
        Directory.CreateDirectory(outputDirectory);

        // Determine the output file name (same as input but placed in the custom directory)
        string outputFileName = Path.GetFileNameWithoutExtension(inputPdf) + "_flattened.pdf";
        string outputPath = Path.Combine(outputDirectory, outputFileName);

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Flatten the document (removes form fields and replaces them with their appearances)
                doc.Flatten();

                // Save the flattened PDF to the custom output directory
                doc.Save(outputPath);
            }

            Console.WriteLine($"Flattened PDF saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error processing PDF: {ex.Message}");
        }
    }
}