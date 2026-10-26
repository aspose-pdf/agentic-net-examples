using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

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

        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle for the button (lower‑left X,Y, upper‑right X,Y)
            Rectangle rect = new Rectangle(100, 700, 200, 750);

            // Create a button form field (acts as a button annotation)
            ButtonField button = new ButtonField(doc.Pages[1], rect)
            {
                PartialName = "ExportAnnotationsBtn",
                Value = "Export Annotations"
            };

            // Simple border
            button.Border = new Border(button) { Width = 1 };

            // JavaScript that gathers all annotations on the page and shows them as JSON
            string js = @"
                var annots = this.getAnnots();
                var data = [];
                for (var i = 0; i < annots.length; i++) {
                    var a = annots[i];
                    data.push({
                        type: a.type,
                        rect: a.rect,
                        contents: a.contents
                    });
                }
                var json = JSON.stringify(data);
                app.alert(json);
            ";

            // Assign the JavaScript to the button's mouse‑press action (valid property)
            button.Actions.OnPressMouseBtn = new JavascriptAction(js);

            // Add the button to the AcroForm and to the page annotations
            doc.Form.Add(button);
            doc.Pages[1].Annotations.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with button annotation: '{outputPath}'");
    }
}
