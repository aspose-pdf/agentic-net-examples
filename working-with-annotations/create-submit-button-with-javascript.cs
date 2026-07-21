using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "submit_button.pdf";
        const string submitUrl = "https://example.com/submit";

        // Create a new PDF document and add a page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Define the button rectangle (llx, lly, urx, ury)
            var btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button field on the page
            var submitButton = new ButtonField(page, btnRect)
            {
                Name = "SubmitBtn",
                AlternateName = "Submit",
                Contents = "Submit"
            };

            // JavaScript that submits the form data to the specified HTTP endpoint
            var jsAction = new JavascriptAction($"this.submitForm({{cURL:'{submitUrl}'}});");

            // Assign the JavaScript to the button's mouse‑release action (valid property for push buttons)
            submitButton.Actions.OnReleaseMouseBtn = jsAction;

            // Add the button to the document's AcroForm collection
            doc.Form.Add(submitButton);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}
