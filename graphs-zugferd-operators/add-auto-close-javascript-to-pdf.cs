using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_autoclose.pdf";
        const int timeoutMs = 5000; // close after 5 seconds

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, add a JavaScript action that closes the document after a timeout,
        // then save the modified PDF.
        using (Document doc = new Document(inputPath))
        {
            // JavaScript to be executed when the document is opened.
            // app.setTimeOut schedules the closeDoc() call after the specified delay (in ms).
            string jsCode = $"app.setTimeOut('this.closeDoc();', {timeoutMs});";
            JavascriptAction jsAction = new JavascriptAction(jsCode);

            // Assign the JavaScript as the document's open action.
            doc.OpenAction = jsAction;

            // Save the updated PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with auto‑close JavaScript saved to '{outputPath}'.");
    }
}
