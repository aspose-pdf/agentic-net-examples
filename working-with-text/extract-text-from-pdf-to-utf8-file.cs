using System;
using System.IO;
using System.Text;
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

        try
        {
            // Load the PDF document
            using (Document pdfDoc = new Document(inputPdfPath))
            {
                // Extract text from all pages
                TextAbsorber absorber = new TextAbsorber();
                pdfDoc.Pages.Accept(absorber);
                string extractedText = absorber.Text ?? string.Empty;

                // Write concatenated text to a UTF-8 file
                File.WriteAllText(outputTxtPath, extractedText, Encoding.UTF8);
            }

            Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}