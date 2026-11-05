using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "expired.pdf";
        const string expiryDate = "2025-12-31"; // YYYY-MM-DD

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // JavaScript that checks the current date against the expiry date.
        string js = $@"
Date exp = new Date('{expiryDate}');
Date now = new Date();
if (now > exp) {{
    app.alert('This document has expired.');
    this.closeDoc(true);
}}
";

        try
        {
            // Load the PDF, embed the JavaScript using OpenAction, and save.
            using (Document doc = new Document(inputPath))
            {
                // Assign JavaScript that runs when the document is opened.
                doc.OpenAction = new JavascriptAction(js);
                doc.Save(outputPath);
            }

            Console.WriteLine($"PDF with expiration saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
