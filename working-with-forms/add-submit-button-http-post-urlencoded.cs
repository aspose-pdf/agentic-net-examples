using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing a form
        const string outputPdf = "output_with_submit.pdf";
        const string submitUrl = "https://example.com/submit"; // target URL

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the submit button will be placed (1‑based index)
            Page page = doc.Pages[1];

            // Define the button rectangle (lower‑left x,y and upper‑right x,y)
            // Use fully qualified type to avoid ambiguity with System.Drawing.Rectangle
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push‑button field on the selected page
            ButtonField submitButton = new ButtonField(page, rect)
            {
                Name = "SubmitBtn",               // internal field name
                AlternateCaption = "Submit Form"  // visible label on the button
            };

            // Configure the submit action:
            // - Use HTTP POST (default, so we do NOT set GetMethod flag)
            // - Export form data in URL‑encoded (HTML form) format
            SubmitFormAction action = new SubmitFormAction
            {
                Url = new FileSpecification(submitUrl),
                Flags = SubmitFormAction.ExportFormat   // HTML form (application/x-www-form-urlencoded)
            };

            // Attach the action to the button
            submitButton.OnActivated = action;

            // Add the button annotation to the page
            page.Annotations.Add(submitButton);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved with configured submit button: {outputPdf}");
    }
}