using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "output.txt";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a TextAbsorber to extract text from the document
            TextAbsorber absorber = new TextAbsorber();

            // Accept the absorber for all pages (pages are 1‑based)
            doc.Pages.Accept(absorber);

            // Get the concatenated text from all pages
            string allText = absorber.Text ?? string.Empty;

            // Write the extracted text to a .txt file
            File.WriteAllText(outputTxt, allText);
        }

        Console.WriteLine($"Extracted text saved to '{outputTxt}'.");
    }
}