using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Input PDF (must contain a form or be a blank page to add the button)
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_submit.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Define the button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push‑button form field
            ButtonField submitBtn = new ButtonField(doc.Pages[1], buttonRect)
            {
                // PartialName is the identifier of the field
                PartialName = "SubmitBtn",
                // NormalCaption sets the visible label on the button
                NormalCaption = "Submit"
            };

            // Configure the submit action – HTTP POST with URL‑encoded form data
            SubmitFormAction submitAction = new SubmitFormAction
            {
                // Url property expects a FileSpecification instance
                Url = new FileSpecification("https://example.com/submit"),
                // ExportFormat flag submits data as HTML form (URL‑encoded).
                // No GetMethod flag → default is HTTP POST.
                Flags = SubmitFormAction.ExportFormat
            };

            // Assign the action to the button (use OnActivated for button press)
            submitBtn.OnActivated = submitAction;

            // Add the button to the document's form collection
            doc.Form.Add(submitBtn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with POST submit button saved to '{outputPath}'.");
    }
}
