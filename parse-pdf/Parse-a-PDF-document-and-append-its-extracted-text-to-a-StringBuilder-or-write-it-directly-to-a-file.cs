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
        const string outputPath = "extracted.txt";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document; using ensures proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextAbsorber to extract text from the document
            TextAbsorber absorber = new TextAbsorber();

            // Accept the absorber for all pages (pages are 1‑based internally)
            doc.Pages.Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text;

            // Optionally accumulate text in a StringBuilder
            StringBuilder sb = new StringBuilder();
            sb.Append(extractedText);

            // Write the accumulated text to a plain‑text file (UTF‑8)
            File.WriteAllText(outputPath, sb.ToString(), Encoding.UTF8);
        }

        Console.WriteLine($"Text extracted to '{outputPath}'.");
    }
}