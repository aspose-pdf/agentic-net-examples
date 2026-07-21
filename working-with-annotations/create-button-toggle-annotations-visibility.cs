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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Choose the page on which the button will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the button rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push‑button field
            ButtonField toggleButton = new ButtonField(page, btnRect)
            {
                Name     = "ToggleAnnotationsBtn",
                Contents = "Toggle Annotations"
            };

            // JavaScript that toggles the Hidden flag of every annotation on the current page
            string jsCode = @"
                var ann = this.getAnnots();
                for (var i = 0; i < ann.length; i++) {
                    ann[i].hidden = !ann[i].hidden;
                }
            ";

            // Attach the JavaScript to the button's mouse‑up action
            JavascriptAction jsAction = new JavascriptAction(jsCode);
            toggleButton.Actions.OnReleaseMouseBtn = jsAction;

            // Add the button to the page's annotation collection
            page.Annotations.Add(toggleButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}