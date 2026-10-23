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
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Add a document‑level JavaScript that saves the PDF every 60 seconds (60000 ms)
            // The script uses app.setInterval to repeatedly invoke this.saveAs().
            // bUI:false hides the save dialog, bSilent:true suppresses alerts,
            // bEmbed:true ensures the document is saved in place.
            string js = "app.setInterval('this.saveAs({bUI:false,bSilent:true,bEmbed:true});', 60000);";
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF (Document.Save writes PDF regardless of extension)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with auto‑save JavaScript saved to '{outputPath}'.");
    }
}
