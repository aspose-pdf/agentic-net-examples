using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // existing PDF with form fields
        const string outputPath = "output_with_reset.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Create a reset button on the first page
            // Rectangle: left, bottom, right, top (coordinates in points)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 540);
            ButtonField resetBtn = new ButtonField(doc, btnRect);

            // Set button appearance (caption)
            resetBtn.NormalCaption = "Reset";
            resetBtn.AlternateCaption = "Reset";

            // Assign a JavaScript action that resets the form when the button is clicked.
            // The OnReleaseMouseBtn action is triggered when the user releases the mouse button over the field.
            resetBtn.Actions.OnReleaseMouseBtn = new JavascriptAction("this.resetForm();");

            // Add the button to the form (page number is 1‑based)
            doc.Form.Add(resetBtn, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with reset button saved to '{outputPath}'.");
    }
}
