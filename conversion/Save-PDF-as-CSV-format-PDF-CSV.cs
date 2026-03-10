using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.csv";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Extract all text from the PDF using TextAbsorber (the correct API for text extraction)
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            string extractedText = absorber.Text ?? string.Empty;

            // Simple CSV export: write the extracted text to a .csv file.
            // For a real CSV you would need to parse the text into rows/columns.
            try
            {
                File.WriteAllText(outputPath, extractedText);
                Console.WriteLine($"PDF content saved as CSV to '{outputPath}'.");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to write CSV file: {ex.Message}");
            }
        }
    }
}