using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // JavascriptAction
using Aspose.Pdf.Forms;        // Form, ButtonField

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form form = doc.Form;

            // Define the button rectangle (lower‑left‑x, lower‑left‑y, upper‑right‑x, upper‑right‑y)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 100, 200, 130);

            // Create a push button field on the document
            ButtonField resetBtn = new ButtonField(doc, btnRect);
            resetBtn.Name        = "ResetButton";      // field name
            resetBtn.PartialName = "ResetButton";
            resetBtn.NormalCaption = "Reset";          // text shown on the button

            // Attach JavaScript that clears all form fields when the button is clicked
            // Use a valid action property – OnPressMouseBtn (or OnReleaseMouseBtn) – instead of the non‑existent OnActivated
            resetBtn.Actions.OnPressMouseBtn = new JavascriptAction("this.resetForm();");

            // Add the button to the form
            form.Add(resetBtn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with reset button saved to '{outputPath}'.");
    }
}
