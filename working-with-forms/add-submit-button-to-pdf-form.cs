using System;
using System.IO;
using System.Drawing;               // Needed for System.Drawing.Color used by DefaultAppearance
using Aspose.Pdf;                     // Core PDF API
using Aspose.Pdf.Forms;               // Form field classes
using Aspose.Pdf.Annotations;         // SubmitFormAction, etc.

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // Existing PDF (must contain a form or be blank)
        const string outputPath = "output_with_submit.pdf";
        const string submitUrl = "https://example.com/submit"; // Destination endpoint

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the button rectangle (llx, lly, urx, ury) – fully qualified to avoid ambiguity
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 700, 200, 730);

            // Create a push‑button (ButtonField) – this is the correct type in Aspose.PDF 26.x
            ButtonField submitButton = new ButtonField(doc.Pages[1], buttonRect)
            {
                PartialName = "SubmitBtn"
            };

            // Set appearance so the caption is visible. DefaultAppearance expects System.Drawing.Color.
            submitButton.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            submitButton.NormalCaption = "Submit";

            // Create a SubmitFormAction – use the parameterless ctor and set the Url property
            SubmitFormAction submitAction = new SubmitFormAction();
            submitAction.Url = new FileSpecification(submitUrl);
            submitButton.OnActivated = submitAction;

            // Add the button to the form on page 1
            doc.Form.Add(submitButton, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}
