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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that checks the current date and closes the document
            // after the specified expiry date (e.g., 2025-12-31).
            string js = @"
                Date expiry = new Date('2025-12-31T23:59:59');
                Date now = new Date();
                if (now > expiry) {
                    app.alert('This document has expired and will be closed.');
                    this.closeDoc();
                }
            ";

            // Assign the JavaScript as a document‑level open action
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with expiry JavaScript: '{outputPath}'");
    }
}