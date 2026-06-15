using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

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

        // Load the existing PDF (using the required load options if any)
        using (Document doc = new Document(inputPath))
        {
            // Create a push button field on the first page
            // Rectangle coordinates: llx, lly, urx, ury
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);
            ButtonField button = new ButtonField(doc.Pages[1], btnRect)
            {
                Name          = "ExportAnnotationsBtn",
                PartialName   = "ExportAnnotationsBtn",
                NormalCaption = "Export Annotations"
            };

            // JavaScript to export all annotations to a JSON string and display it
            // (Here we simply show the JSON in an alert for demonstration)
            JavascriptAction jsAction = new JavascriptAction(
                "app.alert(JSON.stringify(this.getAnnots()));"
            );

            // Assign the JavaScript to the mouse‑press action of the button
            button.Actions.OnPressMouseBtn = jsAction;

            // Add the button to the page's annotation collection
            doc.Pages[1].Annotations.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with button annotation: {outputPath}");
    }
}