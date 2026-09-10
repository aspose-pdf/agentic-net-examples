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

        // Load the PDF, add a document‑level JavaScript action that closes the document
        // after a specific expiry date (e.g., 2025‑12‑31). The script runs when the PDF is opened.
        using (Document doc = new Document(inputPath))
        {
            string script = @"
                Date now = new Date();
                Date expiry = new Date('2025-12-31T23:59:59');
                if (now > expiry) {
                    app.alert('This document has expired.');
                    this.closeDoc();
                }
            ";

            // Assign the JavaScript action to the document's OpenAction property.
            doc.OpenAction = new JavascriptAction(script);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with expiry JavaScript: '{outputPath}'.");
    }
}