using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input_form.pdf";
        const string outputPath = "output_with_reset.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF document (must be inside a using block for proper disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define the rectangle where the reset button will be placed (coordinates are in points)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 100, 200, 130);

            // Create a push button field on the document
            ButtonField resetButton = new ButtonField(doc, buttonRect)
            {
                // Use PartialName to identify the field
                PartialName = "ResetButton",
                // Caption shown on the button
                NormalCaption = "Reset Form",
                // Optional visual appearance
                Color = Aspose.Pdf.Color.LightGray
            };

            // Assign a JavaScript action that resets the whole form when the button is pressed
            resetButton.Actions.OnPressMouseBtn = new JavascriptAction("this.resetForm();");

            // Add the button to the document's form collection
            doc.Form.Add(resetButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with reset button saved to '{outputPath}'.");
    }
}
