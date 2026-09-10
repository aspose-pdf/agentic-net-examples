using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_autosave.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that sets a timer to save the document every 60 seconds (60000 ms)
            // The script uses app.setInterval to repeatedly call this.saveAs.
            // Adjust the interval and file name as needed.
            string js = "app.setInterval('this.saveAs({cPath:\"auto_save.pdf\"});', 60000);";

            // Assign the JavaScript action to be executed when the document is opened.
            // Document‑level JavaScript is set via the OpenAction property.
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Document with auto‑save JavaScript saved to '{outputPath}'.");
    }
}
