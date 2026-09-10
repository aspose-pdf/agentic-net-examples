using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the button rectangle (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 200, 540);

            // Create a push button field on the document
            ButtonField button = new ButtonField(doc, buttonRect);
            button.Name = "ExportAnnotationsBtn";
            button.NormalCaption = "Export JSON";

            // JavaScript that gathers all annotations and shows them as a JSON string
            // (In a real scenario you might write the JSON to a file or a hidden field)
            string jsCode = @"
var annots = this.getAnnots();
var json = JSON.stringify(annots);
app.alert(json);
";
            JavascriptAction jsAction = new JavascriptAction(jsCode);

            // Assign the JavaScript to the button's mouse‑press action
            button.Actions.OnPressMouseBtn = jsAction;

            // Add the button to the PDF form
            doc.Form.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with export button saved to '{outputPath}'.");
    }
}