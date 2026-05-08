using System;
using System.IO;
using System.Text;
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
                // Create a TextAbsorber to extract text from the document
                TextAbsorber absorber = new TextAbsorber();

                // Accept the absorber for all pages
                doc.Pages.Accept(absorber);

                // Retrieve the extracted text (empty string if null)
                string extractedText = absorber.Text ?? string.Empty;

                // Write the text to a UTF‑8 encoded file
                File.WriteAllText(outputPath, extractedText, Encoding.UTF8);
            }

            Console.WriteLine($"Text extracted to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}