using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "extracted.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document; using ensures proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextAbsorber to extract text
            TextAbsorber absorber = new TextAbsorber();

            // Accept the absorber for all pages (Aspose.Pdf uses 1‑based page indexing)
            doc.Pages.Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text;

            // Store the text in a file (optional) and keep it in the variable
            File.WriteAllText(outputPath, extractedText);
            Console.WriteLine($"Text extracted and saved to '{outputPath}'.");
        }
    }
}