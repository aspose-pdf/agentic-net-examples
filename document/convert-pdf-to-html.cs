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
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the source PDF document.
        using (Document doc = new Document(inputPdf))
        {
            // Configure HTML save options. Progress handling is not supported in the current API.
            HtmlSaveOptions saveOptions = new HtmlSaveOptions();

            // Save the document.
            doc.Save(outputHtml, saveOptions);
        }

        // Indicate completion.
        Console.WriteLine("Conversion completed.");
    }
}