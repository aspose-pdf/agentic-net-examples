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

        // Load the PDF document with deterministic disposal
        using (Document pdfDoc = new Document(inputPath))
        {
            // Use TextAbsorber to extract all text from the document
            TextAbsorber absorber = new TextAbsorber();
            pdfDoc.Pages.Accept(absorber);
            string extractedText = absorber.Text;

            // Save the extracted text to a plain text file
            File.WriteAllText(outputPath, extractedText);
        }

        Console.WriteLine($"Text extracted to '{outputPath}'.");
    }
}