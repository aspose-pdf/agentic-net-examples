using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_expiring.pdf";
        // Define the expiry date (YYYY-MM-DD)
        const string expiryDate = "2025-12-31";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // JavaScript executed when the PDF is opened.
        // If the current date is later than the expiry date, the document is closed.
        string js = $"Date today = new Date(); Date expiry = new Date('{expiryDate}'); if (today > expiry) this.closeDoc();";

        using (Document doc = new Document(inputPath))
        {
            // Assign the JavaScript as the document's open action.
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with expiry JavaScript to '{outputPath}'.");
    }
}