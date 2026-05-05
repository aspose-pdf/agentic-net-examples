using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF
        const string outputPdf = "expired.pdf";    // PDF with expiry script
        // Set the desired expiry date (year, month, day)
        DateTime expiryDate = new DateTime(2025, 12, 31);

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // JavaScript that checks the current date against the expiry date.
        // If the document is opened after the expiry date, an alert is shown and the document is closed.
        string jsCode = $@"
var expiry = new Date({expiryDate.Year}, {expiryDate.Month - 1}, {expiryDate.Day});
if (new Date() > expiry) {{
    app.alert('This document has expired on {expiryDate:yyyy-MM-dd}.');
    this.closeDoc();
}}";

        try
        {
            // Open the existing PDF inside a using block for deterministic disposal.
            using (Document doc = new Document(inputPdf))
            {
                // Assign the JavaScript as a document‑level open action.
                doc.OpenAction = new JavascriptAction(jsCode);

                // Save the modified PDF.
                doc.Save(outputPdf);
            }

            Console.WriteLine($"PDF with expiry date saved to '{outputPdf}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}