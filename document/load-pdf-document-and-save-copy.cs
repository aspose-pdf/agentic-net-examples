using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";

        // Verify that the source file exists before attempting to load it
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file into a Document object.
        // The Document(string) constructor opens the file and parses it.
        // Wrapping the Document in a using block ensures deterministic disposal.
        using (Document pdfDoc = new Document(inputPath))
        {
            // At this point the PDF is loaded and ready for further processing.
            Console.WriteLine($"PDF loaded successfully. Page count: {pdfDoc.Pages.Count}");

            // Example operation: save a copy of the loaded document.
            // This demonstrates the required save step in the lifecycle.
            pdfDoc.Save("copy.pdf");
        }
    }
}
