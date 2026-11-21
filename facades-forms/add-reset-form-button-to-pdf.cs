using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // for JavascriptAction
using Aspose.Pdf.Forms;      // for ButtonField and DefaultAppearance

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

        // Load the PDF document
        Document doc = new Document(inputPath);

        // Define button position (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
        Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

        // Create a push button field named "ResetForm"
        ButtonField resetButton = new ButtonField(doc.Pages[1], buttonRect)
        {
            PartialName = "ResetForm",
            // Appearance must be set before setting the caption
            DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black),
            NormalCaption = "Reset"
        };

        // Add the button to the form on page 1
        doc.Form.Add(resetButton, 1);

        // Assign JavaScript that clears all form fields when the button is clicked
        resetButton.OnActivated = new JavascriptAction("this.resetForm();");

        // Save the updated PDF
        doc.Save(outputPath);

        Console.WriteLine($"PDF with ResetForm button saved to '{outputPath}'.");
    }
}
