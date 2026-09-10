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

        // Set the expiration date (year, month (1‑based), day)
        DateTime expireDate = new DateTime(2025, 12, 31);

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Build JavaScript that closes the document after the specified date
            string js = $@"
                var exp = new Date({expireDate.Year}, {expireDate.Month - 1}, {expireDate.Day});
                if (new Date() > exp) this.closeDoc();";

            // Attach the JavaScript as the document's OpenAction
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with expiration date saved to '{outputPath}'.");
    }
}
