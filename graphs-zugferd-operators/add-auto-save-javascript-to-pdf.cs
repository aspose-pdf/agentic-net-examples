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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that saves the document every 30 seconds (30000 ms)
            // app.setInterval repeatedly executes the provided script.
            // this.saveAs saves the current document to the specified path.
            string js = $"app.setInterval('this.saveAs({{cPath:\"{outputPath}\"}});', 30000);";

            // Assign the script to the document's OpenAction using JavascriptAction
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with auto‑save JavaScript saved to '{outputPath}'.");
    }
}
