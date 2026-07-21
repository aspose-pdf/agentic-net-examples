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

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Create a TextAbsorber to extract text from all pages
                TextAbsorber absorber = new TextAbsorber();

                // Accept the absorber for the entire page collection
                doc.Pages.Accept(absorber);

                // Retrieve the concatenated text
                string extractedText = absorber.Text ?? string.Empty;

                // Write the text to a .txt file
                File.WriteAllText(outputTxt, extractedText);
            }

            Console.WriteLine($"Text extracted and saved to '{outputTxt}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}