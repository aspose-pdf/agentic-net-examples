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
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create a push button field on the first page
            ButtonField button = new ButtonField(doc.Pages[1], btnRect)
            {
                Name = "ExportAnnotationsBtn",
                NormalCaption = "Export Annotations",
                Color = Aspose.Pdf.Color.LightGray
            };

            // JavaScript that gathers all annotations on the current page
            // and shows them as a JSON string in an alert dialog.
            string js = @"
var annots = this.getAnnots();
var result = [];
for (var i = 0; i < annots.length; i++) {
    var a = annots[i];
    result.push({
        type: a.type,
        contents: a.contents,
        rect: a.rect
    });
}
app.alert(JSON.stringify(result));
";

            // Assign the JavaScript to the button's mouse‑press action
            button.Actions.OnPressMouseBtn = new JavascriptAction(js);

            // Add the button to the document's form fields collection
            doc.Form.Add(button);

            // Save the modified PDF (using the standard Document.Save method)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with button annotation: {outputPath}");
    }
}