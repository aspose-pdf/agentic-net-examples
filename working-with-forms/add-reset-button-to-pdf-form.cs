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

        // Load the PDF document (lifecycle rule: use using)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form
            Form form = doc.Form;
            if (form == null)
            {
                Console.Error.WriteLine("The PDF does not contain a form.");
                return;
            }

            // Define the rectangle for the reset button
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 100, 200, 130);

            // Create a push button field on the first page
            ButtonField resetButton = new ButtonField(doc, buttonRect)
            {
                Name = "ResetButton",
                PartialName = "ResetButton",
                // Use NormalCaption to set the visible label on the button
                NormalCaption = "Reset",
                // Border color (the annotation's Color property controls the border color)
                Color = Aspose.Pdf.Color.DarkGray
                // Note: BackColor property does not exist on ButtonField in current API version.
            };

            // Set the border (Border requires the parent annotation and has no Color property)
            resetButton.Border = new Border(resetButton) { Width = 1 };

            // Assign a JavaScript action that resets the form to its default values
            // The JavaScript "this.resetForm();" clears all fields and restores defaults.
            resetButton.Actions.OnPressMouseBtn = new JavascriptAction("this.resetForm();");

            // Add the button to the form on page 1 (page indexing is 1‑based)
            form.Add(resetButton, 1);

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with reset button saved to '{outputPath}'.");
    }
}
