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
        const string outputPath = "output_with_reset.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (using rule: document-disposal-with-using)
        using (Document doc = new Document(inputPath))
        {
            // Define the button rectangle (coordinates: llx, lly, urx, ury)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 540);

            // Create a push button field on the document
            ButtonField resetButton = new ButtonField(doc, btnRect)
            {
                PartialName      = "ResetButton",      // internal field name
                AlternateCaption = "Reset Form"        // caption shown on the button
            };

            // Assign a JavaScript action that clears all form fields when the button is clicked
            // Use a valid action property (OnPressMouseBtn) instead of the non‑existent OnMouseUp
            resetButton.Actions.OnPressMouseBtn = new JavascriptAction("this.resetForm();");

            // Add the button to the PDF form (using Form.Add)
            doc.Form.Add(resetButton);

            // Save the modified PDF (using rule: document-disposal-with-using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with reset button saved to '{outputPath}'.");
    }
}
