using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_expired.pdf";
        const string expireDate = "2025-12-31"; // YYYY-MM-DD format

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that closes the document after the specified date
            string js = $"var exp = new Date('{expireDate}'); if (new Date() > exp) this.closeDoc();";

            // Assign the JavaScript to the document's OpenAction (runs when the PDF is opened)
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with expiration script saved to '{outputPath}'.");
    }
}
