using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath  = "input_zugferd.pdf";
        const string outputPath = "output_with_metadata.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the ZUGFeRD PDF inside a using block for proper disposal
            using (Document doc = new Document(inputPath))
            {
                // Add custom metadata entries
                doc.Metadata.Add("ProjectCode", "PRJ-00123");
                doc.Metadata.Add("Department", "Accounting");

                // Save the PDF with the new metadata
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF saved with custom metadata to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}