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

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // JavaScript to display an alert when the document opens
            string script = "app.alert('Document opened!');";

            // Create a JavascriptAction with the script
            JavascriptAction jsAction = new JavascriptAction(script);

            // Assign the action to the document's OpenAction property
            doc.OpenAction = jsAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with JavaScript action: {outputPath}");
    }
}