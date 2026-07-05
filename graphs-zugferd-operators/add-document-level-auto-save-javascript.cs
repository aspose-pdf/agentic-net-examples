using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // JavascriptAction resides here

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that saves the document every 60 seconds
            string js = "app.setInterval('this.saveAs({cPath:\"auto_saved.pdf\"});', 60000);";

            // Create the JavaScript action
            JavascriptAction jsAction = new JavascriptAction(js);

            // Attach the action to run when the document is opened (document‑level open action)
            doc.OpenAction = jsAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with auto‑save JavaScript to '{outputPath}'.");
    }
}
