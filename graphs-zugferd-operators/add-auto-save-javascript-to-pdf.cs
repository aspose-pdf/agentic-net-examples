global using System;
global using System.IO;
global using Aspose.Pdf;
global using Aspose.Pdf.Annotations;

// Program.cs
using Aspose.Pdf.Annotations; // needed for JavascriptAction (kept for clarity)

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_js.pdf";

        // Verify source file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block (ensures proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that triggers an automatic save every 60 000 ms (1 minute)
            // The script uses app.setTimeOut to schedule a saveAs operation.
            string js = $"app.setTimeOut('this.saveAs({{cPath:\"{outputPath}\"}});', 60000);";

            // Assign the script to the document‑level OpenAction (executed when the PDF is opened)
            doc.OpenAction = new JavascriptAction(js);

            // Save the modified PDF (no extra SaveOptions needed for PDF output)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with auto‑save JavaScript to '{outputPath}'.");
    }
}
