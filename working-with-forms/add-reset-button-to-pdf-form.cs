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
        const string outputPath = "output_with_reset.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the reset button will be placed (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 540);

            // Create a push‑button field (ButtonField) on the first page
            ButtonField resetButton = new ButtonField(doc.Pages[1], btnRect)
            {
                // The name used to reference the field programmatically
                Name = "ResetButton",
                // The label shown on the button
                NormalCaption = "Reset Form",
                // Optional: set a background color
                Color = Color.LightGray
            };

            // Set a thin border – this must be done after the object is constructed
            resetButton.Border = new Border(resetButton) { Width = 1 };

            // Attach a JavaScript action that resets the form when the button is pressed
            // Aspose.PDF does not have a dedicated ResetFormAction; use JavaScript instead.
            resetButton.Actions.OnPressMouseBtn = new JavascriptAction("this.resetForm();");

            // Add the button to the form (it will appear on the first page)
            doc.Form.Add(resetButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with reset button saved to '{outputPath}'.");
    }
}
