using System;
using System.IO;
using Aspose.Pdf;                     // Core PDF classes
using Aspose.Pdf.Annotations;        // For JavascriptAction
using Aspose.Pdf.Forms;              // For form field types (ButtonField)

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";      // PDF containing existing form fields
        const string outputPath = "output_with_reset.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has at least one page
            if (doc.Pages.Count == 0)
            {
                Console.Error.WriteLine("The PDF has no pages.");
                return;
            }

            // Create a reset button on the first page.
            // Rectangle is fully qualified to avoid ambiguity with System.Drawing.Rectangle.
            // Coordinates: lower‑left (llx, lly) and upper‑right (urx, ury) in points.
            ButtonField resetButton = new ButtonField(
                doc.Pages[1],
                new Aspose.Pdf.Rectangle(50, 750, 150, 770)   // Position and size of the button
            );

            // Set the field's internal name.
            resetButton.PartialName = "resetButton";
            // Set the caption displayed on the button.
            resetButton.AlternateCaption = "Reset Form";

            // Assign a JavaScript action that clears all form fields when the button is pressed.
            // ButtonField does not expose an "Action" property; use OnActivated (or Actions.OnPressMouseBtn).
            resetButton.OnActivated = new JavascriptAction("this.resetForm();");

            // Add the button to the document's form collection on page 1.
            doc.Form.Add(resetButton, 1);

            // Save the modified PDF.
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with reset button saved to '{outputPath}'.");
    }
}
