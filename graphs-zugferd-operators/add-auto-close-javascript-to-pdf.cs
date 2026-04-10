using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // optional, kept for consistency

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const int timeoutMs = 5000; // close after 5 seconds

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that sets a timeout to close the document
            string js = $"app.setTimeOut('this.closeDoc();', {timeoutMs});";

            // Attach the script to the document's open action (correct property)
            doc.OpenAction = new JavascriptAction(js);
            // Alternatively you could use: doc.Actions.OnOpen = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with auto‑close script to '{outputPath}'.");
    }
}
