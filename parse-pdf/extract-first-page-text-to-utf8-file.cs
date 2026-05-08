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

        // Load the PDF document (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Extract text from the first page using TextAbsorber (text‑extraction rule)
            TextAbsorber absorber = new TextAbsorber();
            doc.Pages[1].Accept(absorber); // Pages are 1‑based
            string extractedText = absorber.Text;

            // Write the extracted text to a UTF‑8 encoded file
            File.WriteAllText(outputTxt, extractedText, Encoding.UTF8);
        }

        Console.WriteLine($"Text extracted from first page saved to '{outputTxt}'.");
    }
}