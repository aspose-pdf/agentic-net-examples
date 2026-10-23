using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js.pdf";
        const int timeoutMs = 5000; // close after 5 seconds

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and ensure deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add JavaScript that closes the document after the specified timeout.
            // Use OpenAction with a JavascriptAction because Document.JavaScript is read‑only.
            string script = $"app.setTimeOut('this.closeDoc();', {timeoutMs});";
            doc.OpenAction = new JavascriptAction(script);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with auto‑close JavaScript to '{outputPath}'.");
    }
}
