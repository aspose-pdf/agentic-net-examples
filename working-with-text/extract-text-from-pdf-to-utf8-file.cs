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

        // Load the PDF document (wrapped in using for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Extract text from all pages using TextAbsorber
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            string extractedText = absorber.Text;

            // Write the concatenated text to a UTF-8 encoded file
            File.WriteAllText(outputPath, extractedText, Encoding.UTF8);
        }

        Console.WriteLine($"All text extracted to '{outputPath}'.");
    }
}