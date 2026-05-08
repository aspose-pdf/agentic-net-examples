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
            // Load the PDF document (core API)
            using (Document doc = new Document(inputPath))
            {
                // Create a TextAbsorber to extract text
                TextAbsorber absorber = new TextAbsorber();

                // Configure extraction options to get only visible text
                // The constructor requires a TextFormattingMode enum value.
                TextExtractionOptions extractOpts = new TextExtractionOptions(TextExtractionOptions.TextFormattingMode.Pure);
                // If the API provides a flag for hidden text, ensure it is disabled:
                // extractOpts.ExtractHiddenText = false; // (uncomment if available)

                absorber.ExtractionOptions = extractOpts;

                // Apply the absorber to the whole document (all pages)
                absorber.Visit(doc);

                // Write the extracted visible text to a .txt file
                File.WriteAllText(outputPath, absorber.Text);
                Console.WriteLine($"Visible text extracted to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
