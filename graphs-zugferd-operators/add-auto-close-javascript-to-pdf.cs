using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "auto_close.pdf";
        const int timeoutMs = 5000; // close after 5 seconds

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure deterministic disposal of the Document
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that closes the PDF after the specified timeout.
            // Use OpenAction with a JavascriptAction because Document.JavaScript is read‑only.
            string js = $"app.setTimeOut('this.closeDoc();', {timeoutMs});";
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with auto‑close script to '{outputPath}'.");
    }
}
