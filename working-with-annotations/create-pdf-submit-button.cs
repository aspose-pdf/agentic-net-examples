using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "button_submit_form.pdf";
        const string submitUrl   = "https://example.com/submit";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 600, 250, 650);

            // Create a push button field on the page
            ButtonField submitButton = new ButtonField(page, btnRect)
            {
                // Optional visual properties
                Color        = Aspose.Pdf.Color.LightGray,
                Contents     = "Submit",
                // Set a name for the field (used in form data)
                PartialName  = "SubmitBtn"
            };

            // JavaScript that submits the form to the specified URL
            // Using the Acrobat JavaScript API: this.submitForm({cURL:'url'});
            JavascriptAction jsAction = new JavascriptAction(
                $"this.submitForm({{cURL:'{submitUrl}'}});"
            );

            // Assign the JavaScript action to the button's activation event
            submitButton.OnActivated = jsAction;

            // Add the button to the document's form collection.
            // The constructor already links the field to the page, so we must NOT add it to page.Annotations.
            doc.Form.Add(submitButton);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}
