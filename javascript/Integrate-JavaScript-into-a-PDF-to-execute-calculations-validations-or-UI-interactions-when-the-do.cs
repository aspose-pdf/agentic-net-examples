using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // JavascriptAction resides here

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

        // Load the existing PDF inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // JavaScript that will run when the document is opened
            string jsCode = @"
                app.alert('Document loaded. Performing calculation...');
                var a = 10;
                var b = 20;
                var sum = a + b;
                console.println('Sum of a and b = ' + sum);
            ";

            JavascriptAction jsAction = new JavascriptAction(jsCode);

            // Assign the JavaScript action to the document's open action
            // This is the correct way to embed document‑level JavaScript.
            doc.OpenAction = jsAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript saved to '{outputPath}'.");
    }
}
