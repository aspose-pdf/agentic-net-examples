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

        // Load PDF document
        using (Document pdfDocument = new Document(inputPath))
        {
            // Extract text using TextAbsorber
            TextAbsorber absorber = new TextAbsorber();
            pdfDocument.Pages.Accept(absorber);
            string extracted = absorber.Text;

            // Store in StringBuilder for further manipulation
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(extracted);
            // Example manipulation: collapse double spaces
            string processed = sb.ToString().Replace("  ", " ");

            // Write the processed text to a file
            File.WriteAllText(outputPath, processed);
            Console.WriteLine($"Extracted text saved to '{outputPath}'.");
        }
    }
}