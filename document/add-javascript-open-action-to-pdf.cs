using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;

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

        // Load the existing PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a JavaScript action that shows an alert when the document opens
            JavascriptAction openJs = new JavascriptAction("app.alert('Document opened');");

            // Assign the action to the document's OpenAction property
            doc.OpenAction = openJs;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        // Use fully‑qualified System.IO.Path to avoid any possible ambiguity with Aspose.Pdf.Drawing.Path
        Console.WriteLine($"PDF saved with JavaScript open action: '{System.IO.Path.GetFullPath(outputPath)}'.");
    }
}
