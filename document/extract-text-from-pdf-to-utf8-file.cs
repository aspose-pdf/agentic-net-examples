using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";
        const string outputTxtPath = "output.txt";

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPdfPath))
        {
            // Create a TextAbsorber to extract text from the whole document
            TextAbsorber absorber = new TextAbsorber();

            // Accept the absorber for all pages
            doc.Pages.Accept(absorber);

            // Retrieve the extracted text
            string extractedText = absorber.Text;

            // Write the text to a UTF-8 encoded file
            File.WriteAllText(outputTxtPath, extractedText, Encoding.UTF8);
        }

        Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
    }
}