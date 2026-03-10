using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document with deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a TextAbsorber to extract text from the document
            TextAbsorber absorber = new TextAbsorber();

            // Apply the absorber to all pages of the document
            doc.Pages.Accept(absorber);

            // Retrieve the extracted text via the Text property
            string extractedText = absorber.Text;

            // Output the extracted text (or process as needed)
            Console.WriteLine("Extracted Text:");
            Console.WriteLine(extractedText);
        }
    }
}