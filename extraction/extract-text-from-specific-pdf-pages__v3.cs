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
            if (doc.Pages.Count < 12)
            {
                Console.Error.WriteLine("Document does not contain pages 11 and 12.");
                return;
            }

            TextAbsorber absorber = new TextAbsorber();

            doc.Pages[11].Accept(absorber);
            doc.Pages[12].Accept(absorber);

            string combinedText = absorber.Text ?? string.Empty;
            File.WriteAllText(outputPath, combinedText);
        }

        Console.WriteLine($"Extracted text saved to '{outputPath}'.");
    }
}