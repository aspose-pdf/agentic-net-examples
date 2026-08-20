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

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the reset button will be placed
            // Fully qualified to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 540);

            // Create a push button field on the document
            ButtonField resetButton = new ButtonField(doc, btnRect)
            {
                // Set the button caption (text shown on the button)
                AlternateCaption = "Reset",
                // Optional: set a tooltip for the button
                AlternateName = "ResetFormButton"
            };

            // Attach a JavaScript action that clears all form fields when the button is clicked
            // Use a valid action property from AnnotationActionCollection (OnPressMouseBtn or OnReleaseMouseBtn)
            resetButton.Actions.OnPressMouseBtn = new JavascriptAction("this.resetForm();");

            // Add the button to the PDF form
            doc.Form.Add(resetButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with reset button saved to '{outputPath}'.");
    }
}
