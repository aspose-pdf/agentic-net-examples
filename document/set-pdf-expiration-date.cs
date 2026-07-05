using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // source PDF
        const string outputPath = "expired_output.pdf"; // PDF with expiration script
        const string expireDate = "2025-12-31";         // desired expiration date (YYYY-MM-DD)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that closes the document if the current date is past the expiration date
            string js = $"var exp = new Date('{expireDate}'); if (new Date() > exp) this.closeDoc();";

            // Attach the script as a document‑level OpenAction (executed when the document is opened)
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF (PDF format is implicit)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with expiration script saved to '{outputPath}'.");
    }
}
