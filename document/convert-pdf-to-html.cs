using System;
using System.IO;
using Aspose.Pdf;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputHtml = "output.html";

        if (!File.Exists(inputPdf))
        {
            Console.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document using the core Document API
        using (Document doc = new Document(inputPdf))
        {
            // Set up HTML conversion options (no progress handler – not supported)
            HtmlSaveOptions saveOptions = new HtmlSaveOptions();
            doc.Save(outputHtml, saveOptions);
        }

        Console.WriteLine("Conversion completed.");
    }
}
