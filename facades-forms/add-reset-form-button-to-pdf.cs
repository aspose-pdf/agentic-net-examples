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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF
        Document doc = new Document(inputPath);

        // Define the button rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
        Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 700, 200, 730);

        // Create a button field named "ResetForm"
        ButtonField resetButton = new ButtonField(doc.Pages[1], buttonRect)
        {
            PartialName = "ResetForm",
            Value = "Reset"
        };

        // Attach JavaScript that clears all form fields when the button is clicked
        // Use a valid action property (OnPressMouseBtn or OnReleaseMouseBtn) instead of the non‑existent OnMouseUp
        resetButton.Actions.OnPressMouseBtn = new JavascriptAction("this.resetForm();");

        // Add the button to the form (page index is 1‑based)
        doc.Form.Add(resetButton, 1);

        // Save the modified PDF
        doc.Save(outputPath);

        Console.WriteLine($"PDF with ResetForm button saved to '{outputPath}'.");
    }
}