using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify that the file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into a Document instance.
        // The Document is wrapped in a using block to ensure deterministic disposal.
        using (Document pdfDoc = new Document(inputPath))
        {
            // At this point the PDF is loaded and ready for further processing.
            Console.WriteLine($"PDF loaded successfully. Page count: {pdfDoc.Pages.Count}");

            // Example placeholder for additional manipulation:
            // pdfDoc.Pages[1].Paragraphs.Add(new TextFragment("Hello, Aspose.Pdf!"));
        }

        // The Document has been disposed automatically here.
    }
}