using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string baseUrl = "https://www.example.com";

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
                // Set the BaseURL property in the XMP metadata via the Metadata dictionary
                // The key follows the XMP namespace prefix "xmp".
                doc.Metadata["xmp:BaseURL"] = baseUrl;

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"BaseUrl set and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
