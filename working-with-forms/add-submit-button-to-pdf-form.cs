using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // SubmitFormAction
using Aspose.Pdf.Forms;        // ButtonField

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // existing PDF with a form
        const string outputPdf = "output_with_submit.pdf";
        const string submitUrl = "https://example.com/submit"; // endpoint to receive form data

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document (lifecycle rule: use using for deterministic disposal)
        using (Document doc = new Document(inputPdf))
        {
            // Create a push‑button field on page 1
            // Rectangle constructor: (llx, lly, urx, ury)
            var submitButton = new Aspose.Pdf.Forms.ButtonField(
                doc,
                new Aspose.Pdf.Rectangle(100, 500, 200, 550));

            submitButton.PartialName      = "SubmitButton";
            submitButton.NormalCaption    = "Submit";
            submitButton.AlternateCaption = "Submit";

            // Configure the submit‑form action
            var action = new SubmitFormAction
            {
                // Url property expects a FileSpecification, not a raw string
                Url   = new FileSpecification(submitUrl),
                // Export field data as an HTML form (default is FDF)
                Flags = SubmitFormAction.ExportFormat
            };

            // Attach the action to the button – use a valid action property
            submitButton.Actions.OnPressMouseBtn = action;

            // Add the button to the form on page 1 (lifecycle rule: use Form.Add)
            doc.Form.Add(submitButton, 1);

            // Save the modified PDF (lifecycle rule: save inside using block)
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPdf}'.");
    }
}
