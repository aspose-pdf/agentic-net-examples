using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "auto_save.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that saves the document every 60 seconds (60000 ms)
            // The script runs when the PDF is opened (OpenAction)
            string js = @"
var autoSave = function() {
    this.saveAs({cPath:'" + Path.GetFileName(outputPath).Replace("'", "\\'") + @"'});
    app.setTimeout(autoSave, 60000);
};
app.setTimeout(autoSave, 60000);
";

            // Attach the JavaScript to the document's OpenAction
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with auto‑save JavaScript saved to '{outputPath}'.");
    }
}