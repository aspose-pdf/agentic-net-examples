using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the button rectangle (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button field on the first page
            ButtonField button = new ButtonField(doc.Pages[1], buttonRect)
            {
                Name = "ExportAnnotationsBtn",
                PartialName = "ExportAnnotationsBtn",
                NormalCaption = "Export JSON"
            };

            // JavaScript that gathers all annotations, converts them to JSON, and shows the result
            string jsCode = @"
var ann = this.getAnnots();
if (ann != null) {
    var json = JSON.stringify(ann);
    app.alert(json);
} else {
    app.alert('No annotations found.');
}";
            JavascriptAction jsAction = new JavascriptAction(jsCode);

            // Assign the JavaScript to the button's mouse‑press action
            button.Actions.OnPressMouseBtn = jsAction;

            // Add the button to the document's form fields collection
            doc.Form.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with button annotation: {outputPath}");
    }
}