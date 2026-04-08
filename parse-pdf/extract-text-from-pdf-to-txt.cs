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

        // Verify the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a TextAbsorber to collect text from the document
            TextAbsorber absorber = new TextAbsorber();

            // Accept the absorber for all pages of the document
            doc.Pages.Accept(absorber);

            // Retrieve the concatenated text (empty string if null)
            string allText = absorber.Text ?? string.Empty;

            // Write the extracted text to a plain .txt file
            File.WriteAllText(outputTxt, allText);
        }

        Console.WriteLine($"Extracted text saved to '{outputTxt}'.");
    }
}