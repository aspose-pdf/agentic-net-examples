using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

const string inputPath = "input.pdf";
const string outputPath = "reset_form.pdf";

if (!File.Exists(inputPath))
{
    Console.Error.WriteLine($"File not found: {inputPath}");
    return;
}

using (Document doc = new Document(inputPath))
{
    // Define the button rectangle (lower‑left X,Y, upper‑right X,Y)
    Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

    // Create a push‑button field on the first page
    ButtonField resetButton = new ButtonField(doc.Pages[1], btnRect)
    {
        PartialName = "ResetAllFields",
        NormalCaption = "Reset Form"
    };

    // Assign a JavaScript action that resets the form
    resetButton.Actions.OnPressMouseBtn = new JavascriptAction("this.resetForm();");

    // Add the button to the document's form fields collection
    doc.Form.Add(resetButton, 1);

    // Save the modified PDF
    doc.Save(outputPath);
}

Console.WriteLine($"PDF with reset button saved to '{outputPath}'.");