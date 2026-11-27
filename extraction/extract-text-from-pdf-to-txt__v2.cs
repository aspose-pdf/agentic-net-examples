using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // TextAbsorber extracts text from the document
                TextAbsorber absorber = new TextAbsorber();

                // Optional: configure extraction to get pure text without formatting
                absorber.ExtractionOptions = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);

                // Apply the absorber to all pages
                doc.Pages.Accept(absorber);

                // Write the extracted text to a plain text file
                File.WriteAllText(outputPath, absorber.Text);
            }

            Console.WriteLine($"Text successfully extracted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}