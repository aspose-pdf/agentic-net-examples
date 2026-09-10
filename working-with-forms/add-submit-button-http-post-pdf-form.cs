using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input_form.pdf";   // existing PDF with a form (or blank PDF)
        const string outputPdf = "output_with_submit.pdf";
        const string submitUrl = "https://example.com/submit";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Define the rectangle for the submit button (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 540);

            // Create a push button field and add it to the form
            ButtonField submitBtn = new ButtonField(doc, btnRect)
            {
                PartialName   = "submitBtn",          // internal field name
                NormalCaption = "Submit",            // text shown on the button
                Color         = Aspose.Pdf.Color.LightGray
            };
            doc.Form.Add(submitBtn);

            // Create a SubmitFormAction
            SubmitFormAction submitAction = new SubmitFormAction
            {
                // ExportFormat flag makes the data URL‑encoded (HTML form format)
                Flags = SubmitFormAction.ExportFormat,
                // Destination URL for the form submission
                Url   = new FileSpecification(submitUrl)
            };

            // Attach the action to the button using a valid action property
            // (AnnotationActionCollection does not have an Add method)
            submitBtn.Actions.OnPressMouseBtn = submitAction;

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPdf}'.");
    }
}
