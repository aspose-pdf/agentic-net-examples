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

        using (Document doc = new Document(inputPath))
        {
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages.Accept(absorber);
            string extractedText = absorber.Text ?? string.Empty;
            File.WriteAllText(outputPath, extractedText, System.Text.Encoding.UTF8);
        }

        Console.WriteLine($"Extracted text saved to '{outputPath}'.");
    }
}