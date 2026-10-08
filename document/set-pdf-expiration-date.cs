using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // Required for JavascriptAction

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "expiring_output.pdf";
        // Desired expiration date (year, month, day)
        DateTime expirationDate = new DateTime(2025, 12, 31);

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Build JavaScript that closes the document after the expiration date
            string js = $@"var now = new Date();
var exp = new Date('{expirationDate:yyyy-MM-dd}');
if (now > exp) this.closeDoc();";

            // Attach the script to the document's OpenAction (executed when the PDF is opened)
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with expiration date saved to '{outputPath}'.");
    }
}