using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "button_submit.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Define the button rectangle (lower‑left X/Y, upper‑right X/Y)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button field on the page
            ButtonField submitButton = new ButtonField(page, buttonRect);
            submitButton.Name = "SubmitButton";          // internal field name
            submitButton.Contents = "Submit";            // visible caption
            submitButton.Color = Aspose.Pdf.Color.LightGray; // button background color

            // JavaScript that submits the form data to the specified HTTP endpoint
            string jsCode = "this.submitForm({cURL:'https://example.com/submit'});";

            // Attach the JavaScript action to the button's activation event
            submitButton.OnActivated = new JavascriptAction(jsCode);

            // Add the button field to the document's form collection (not to the page)
            doc.Form.Add(submitButton);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}
