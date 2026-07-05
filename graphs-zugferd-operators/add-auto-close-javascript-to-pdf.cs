using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // JavascriptAction resides in this namespace

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // source PDF
        const string outputPath = "output_with_js.pdf"; // PDF with auto‑close script
        const int timeoutMs    = 5000;                  // close after 5 seconds

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF, add a JavaScript action that closes the document after the timeout,
        // and save the result.
        using (Document doc = new Document(inputPath))
        {
            // JavaScript to schedule document close after the specified timeout.
            // app.setTimeOut executes the code after the given milliseconds.
            string js = $"app.setTimeOut('this.closeDoc();', {timeoutMs});";

            // Assign the script as the document's OpenAction so it runs when the PDF is opened.
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with auto‑close JavaScript to '{outputPath}'.");
    }
}