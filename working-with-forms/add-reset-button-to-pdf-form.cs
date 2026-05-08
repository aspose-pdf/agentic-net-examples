using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // JavascriptAction
using Aspose.Pdf.Forms;        // ButtonField

class Program
{
    static void Main()
    {
        const string inputPath  = "input_form.pdf";
        const string outputPath = "output_with_reset.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF that contains a form
        using (Document doc = new Document(inputPath))
        {
            // Create a push‑button field that will act as the Reset button
            // Rectangle coordinates: lower‑left (x1, y1), upper‑right (x2, y2)
            ButtonField resetButton = new ButtonField(
                doc,
                new Aspose.Pdf.Rectangle(100, 500, 200, 550));

            // Set a name for the field (used to reference it programmatically)
            resetButton.PartialName = "ResetFormButton";

            // Optional visual properties
            resetButton.AlternateCaption = "Reset";
            resetButton.Color = Aspose.Pdf.Color.LightGray;
            resetButton.Border = new Border(resetButton) { Width = 1 };

            // Assign a JavaScript action that resets the form when the button is clicked.
            // Use a valid action property – OnPressMouseBtn (or OnReleaseMouseBtn).
            resetButton.Actions.OnPressMouseBtn = new JavascriptAction("this.resetForm();");

            // Add the button to the document's form collection
            doc.Form.Add(resetButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with reset button saved to '{outputPath}'.");
    }
}
