using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

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

        // Load the existing PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Create a reset button field on the first page
            var resetButton = new ButtonField(
                doc.Pages[1],
                new Aspose.Pdf.Rectangle(100, 700, 200, 750))
            {
                // Field name (PartialName is the correct property for AcroForm fields)
                PartialName = "ResetButton",
                // The label shown on the button
                NormalCaption = "Reset Form"
            };

            // Use a JavaScript action to reset the form when the button is pressed
            resetButton.OnActivated = new JavascriptAction("this.resetForm();");

            // Add the button to the document's form collection
            doc.Form.Add(resetButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with reset button saved to '{outputPath}'.");
    }
}