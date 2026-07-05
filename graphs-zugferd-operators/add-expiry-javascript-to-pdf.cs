using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_expiry.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF, add a document‑level JavaScript action, and save.
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that checks the current date against an expiry date.
            // If the current date is later than the expiry, show an alert and close the document.
            string js = @"
                Date expiry = new Date('2026-12-31T23:59:59Z');
                if (new Date() > expiry) {
                    app.alert('This document has expired and will be closed.');
                    this.closeDoc();
                }
            ";

            // Assign the JavaScript to the document's OpenAction so it runs when the PDF is opened.
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with expiry JavaScript: '{outputPath}'.");
    }
}