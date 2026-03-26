using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Expiration date in ISO format (YYYY-MM-DD)
        string expirationDate = "2025-12-31";

        // JavaScript that closes the document after the expiration date
        string js = $"var now = new Date(); var expiry = new Date('{expirationDate}'); " +
                    "if (now > expiry) { app.alert('Document has expired.'); this.closeDoc(true); }";

        using (Document doc = new Document(inputPath))
        {
            // Set JavaScript to run when the document is opened
            doc.OpenAction = new JavascriptAction(js);
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with expiration saved to '{outputPath}'.");
    }
}
