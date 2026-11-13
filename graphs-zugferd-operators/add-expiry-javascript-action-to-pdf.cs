using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Required for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "expiry.pdf";
        const string expiryDate = "2025-12-31"; // YYYY-MM-DD format

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // JavaScript that runs when the document is opened.
                // It compares the current date with the expiry date and
                // shows an alert and closes the document if the expiry has passed.
                string js = $@"
Date expiry = new Date('{expiryDate}');
Date now = new Date();
if (now > expiry) {{
    app.alert('This document has expired.');
    this.closeDoc();
}}";

                // Assign the JavaScript as the document's OpenAction
                doc.OpenAction = new JavascriptAction(js);

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with expiry JavaScript saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
