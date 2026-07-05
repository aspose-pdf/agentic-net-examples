using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        // Input OFD file and desired PDF output paths
        const string inputPath = "input.ofd";
        const string outputPath = "output.pdf";

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the OFD file using the appropriate load options
            using (Document doc = new Document(inputPath, new OfdLoadOptions()))
            {
                // Save the loaded document as PDF (default settings)
                doc.Save(outputPath);
            }

            Console.WriteLine($"Successfully converted OFD to PDF: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during loading or saving
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}