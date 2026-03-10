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

        // Verify that the input PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextAbsorber to extract text from the document
            TextAbsorber absorber = new TextAbsorber();

            // Apply the absorber to all pages of the document
            doc.Pages.Accept(absorber);

            // Retrieve the extracted plain text
            string extractedText = absorber.Text;

            // Write the extracted text to a plain text file
            File.WriteAllText(outputPath, extractedText);
        }

        Console.WriteLine($"Text extracted to '{outputPath}'.");
    }
}