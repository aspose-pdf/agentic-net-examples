using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "output.txt";

        // Verify that the source PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document; the using block ensures deterministic disposal
        using (Document doc = new Document(inputPdf))
        {
            // Create a TextAbsorber to collect text from all pages
            TextAbsorber absorber = new TextAbsorber();

            // Apply the absorber to the document's pages
            doc.Pages.Accept(absorber);

            // Retrieve the concatenated text (empty string if null)
            string extractedText = absorber.Text ?? string.Empty;

            // Write the result to a UTF‑8 encoded text file
            File.WriteAllText(outputTxt, extractedText, Encoding.UTF8);
        }

        Console.WriteLine($"All text extracted to '{outputTxt}'.");
    }
}