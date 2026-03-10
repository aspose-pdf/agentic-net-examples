using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.ofd";   // OFD source file
        const string outputPath = "output.pdf"; // Desired PDF output

        // Verify the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load OFD using the appropriate load options
            using (Document doc = new Document(inputPath, new OfdLoadOptions()))
            {
                // If the document carries PDF/A compliance, remove it to get a regular PDF
                doc.RemovePdfaCompliance();

                // Save the document as a standard PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Conversion completed: '{outputPath}'");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during conversion: {ex.Message}");
        }
    }
}