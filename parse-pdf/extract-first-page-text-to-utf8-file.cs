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

        using (Document doc = new Document(inputPdf))
        {
            // Extract text from the first page
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages[1].Accept(absorber);
            string extractedText = absorber.Text;

            // Write text to UTF-8 encoded file
            File.WriteAllText(outputTxt, extractedText, Encoding.UTF8);
        }

        Console.WriteLine($"Extracted text saved to '{outputTxt}'.");
    }
}