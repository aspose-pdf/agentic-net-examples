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

        // JavaScript that checks the current date against an expiry date.
        string js = @"
Date today = new Date();
Date expiry = new Date('2025-12-31T23:59:59');
if (today > expiry) {
    app.alert('This document has expired.');
    this.closeDoc();
}
";

        using (Document doc = new Document(inputPath))
        {
            // Assign the JavaScript to run when the document is opened.
            doc.OpenAction = new JavascriptAction(js);

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with expiry action saved to '{outputPath}'.");
    }
}