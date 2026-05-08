using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Paths to the source and destination PDF files
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        // Timeout in milliseconds after which the PDF window will close automatically
        const int closeTimeoutMs = 5000; // 5 seconds

        // JavaScript code that sets a timeout to close the document
        string jsCode = $"app.setTimeOut('this.closeDoc();', {closeTimeoutMs});";

        // Ensure the source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Load the PDF, attach the JavaScript action, and save the result
        using (Aspose.Pdf.Document doc = new Aspose.Pdf.Document(inputPath))
        {
            // Assign the JavaScript to be executed when the document is opened
            doc.OpenAction = new Aspose.Pdf.Annotations.JavascriptAction(jsCode);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with auto‑close JavaScript saved to '{outputPath}'.");
    }
}