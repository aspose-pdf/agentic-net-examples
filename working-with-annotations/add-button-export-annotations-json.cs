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
        const string outputPath = "output_with_button.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (lifecycle rule: using block for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Use the first page as the container for the button
            Page page = doc.Pages[1];

            // Define the button rectangle (coordinates are in points)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button field on the page
            ButtonField button = new ButtonField(page, rect)
            {
                Name = "ExportButton",               // internal field name
                NormalCaption = "Export Annotations" // text shown on the button
            };

            // JavaScript that attempts to export all annotations to a JSON string.
            // This is a simple example; actual PDF JavaScript may need more logic.
            string js = @"
var annots = this.getAnnots();
var json = JSON.stringify(annots);
console.println(json);
";

            // Assign the JavaScript to the button's mouse‑press action
            button.Actions.OnPressMouseBtn = new JavascriptAction(js);

            // Add the button to the document's form fields
            doc.Form.Add(button);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with button annotation: {outputPath}");
    }
}