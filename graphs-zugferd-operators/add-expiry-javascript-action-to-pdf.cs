using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_expiry.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, add a document‑level JavaScript that enforces an expiry date,
        // and save the modified document.
        using (Document doc = new Document(inputPath))
        {
            // JavaScript to be executed when the PDF is opened.
            // It checks the current date against the expiry date and, if expired,
            // shows an alert and closes the document.
            string expiryScript = @"
Date expiry = new Date('2025-12-31');
if (new Date() > expiry) {
    app.alert('This document has expired.');
    this.closeDoc(true);
}";

            // Assign the script as the document's OpenAction.
            doc.OpenAction = new JavascriptAction(expiryScript);

            // Save the updated PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with expiry JavaScript saved to '{outputPath}'.");
    }
}
