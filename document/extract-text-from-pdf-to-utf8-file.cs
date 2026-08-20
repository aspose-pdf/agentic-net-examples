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

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPdf))
            {
                // Extract text from all pages
                TextAbsorber absorber = new TextAbsorber();
                doc.Pages.Accept(absorber);
                string extractedText = absorber.Text ?? string.Empty;

                // Write the extracted text to a UTF‑8 encoded file
                File.WriteAllText(outputTxt, extractedText, Encoding.UTF8);
            }

            Console.WriteLine($"Text successfully extracted to '{outputTxt}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}