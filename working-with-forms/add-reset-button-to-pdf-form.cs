using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // JavascriptAction
using Aspose.Pdf.Forms;      // ButtonField

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
            // Access the AcroForm object
            Form form = doc.Form;

            // Define the rectangle for the reset button
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push‑button field
            ButtonField resetButton = new ButtonField(doc, btnRect)
            {
                PartialName = "ResetButton",
                AlternateCaption = "Reset Form"
            };

            // Attach a JavaScript action that resets the form when the button is clicked
            // Use the correct action property for a button click: OnReleaseMouseBtn
            resetButton.Actions.OnReleaseMouseBtn = new JavascriptAction("this.resetForm();");

            // Add the button to the form
            form.Add(resetButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with reset button saved to '{outputPath}'.");
    }
}
