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
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Extract all text from the document using TextAbsorber (default Unicode encoding)
            TextAbsorber absorber = new TextAbsorber();
            pdfDoc.Pages.Accept(absorber);
            string extractedText = absorber.Text;

            // Write the extracted text to a file using the default encoding (UTF-8)
            File.WriteAllText(outputTxtPath, extractedText, Encoding.UTF8);
        }

        Console.WriteLine($"Text extracted to '{outputTxtPath}'.");
    }
}