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

        // Load the PDF, set a document‑level JavaScript action, and save.
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that closes the document after the specified timeout.
            string js = $"app.setTimeOut('this.closeDoc();', {timeoutMs});";

            // Assign the script to the document's open action.
            // Document.OpenAction is the correct way to add a document‑level script.
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with auto‑close script saved to '{outputPath}'.");
    }
}
