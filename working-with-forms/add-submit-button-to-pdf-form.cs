using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;   // SubmitFormAction, FileSpecification
using Aspose.Pdf.Forms;        // ButtonField

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";          // existing PDF with form fields
        const string outputPath = "output_with_submit.pdf";
        const string submitUrl  = "https://example.com/submit"; // endpoint to receive form data

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document (lifecycle rule: wrap in using)
        using (Document doc = new Document(inputPath))
        {
            // Choose the page where the button will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button field on the selected page
            ButtonField submitButton = new ButtonField(page, btnRect)
            {
                Name        = "SubmitButton",   // internal field name
                PartialName = "SubmitButton",   // name shown in the PDF UI
                // Optional visual properties
                Color       = Aspose.Pdf.Color.LightGray,
                Contents    = "Submit"
            };

            // Create a SubmitFormAction that posts form data to the specified URL
            SubmitFormAction submitAction = new SubmitFormAction
            {
                // Url expects a FileSpecification, not a raw string
                Url   = new FileSpecification(submitUrl),
                // Export field data as an HTML form (standard web submission)
                Flags = SubmitFormAction.ExportFormat
            };

            // Assign the action to the button's mouse‑press event (valid property for ButtonField)
            submitButton.Actions.OnPressMouseBtn = submitAction;

            // Add the button to the document's form collection
            doc.Form.Add(submitButton);

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}
