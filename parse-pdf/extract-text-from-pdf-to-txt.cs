using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath  = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Create a TextAbsorber to extract text from all pages
            TextAbsorber absorber = new TextAbsorber();

            // Accept the absorber for the entire Pages collection
            pdfDoc.Pages.Accept(absorber);

            // Retrieve the concatenated text
            string extractedText = absorber.Text;

            // Write the text to a .txt file
            File.WriteAllText(outputTxtPath, extractedText);
        }

        Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
    }
}