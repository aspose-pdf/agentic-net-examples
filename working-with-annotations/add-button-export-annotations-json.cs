using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_button.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create a push button field on the first page
            ButtonField button = new ButtonField(doc.Pages[1], buttonRect)
            {
                // Optional visual properties
                Color   = Aspose.Pdf.Color.LightGray,
                Contents = "Export Annotations"
            };

            // JavaScript to export all annotations to a JSON string
            // (PDF JavaScript API: this.getAnnots() returns an array of annotation objects)
            string jsCode = @"
                var annots = this.getAnnots();
                var json = JSON.stringify(annots);
                // For demonstration, show the JSON in an alert dialog
                app.alert(json);
            ";

            // Assign the JavaScript action to the button's mouse‑press event
            button.Actions.OnPressMouseBtn = new JavascriptAction(jsCode);

            // Add the button to the document's form fields collection
            doc.Form.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with button annotation: {outputPath}");
    }
}