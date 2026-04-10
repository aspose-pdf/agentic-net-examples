using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string submitUrl = "https://example.com/submit";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the existing PDF
        using (Document doc = new Document(inputPdf))
        {
            // Define the button rectangle (llx, lly, urx, ury)
            // Use the fully‑qualified Aspose.Pdf.Rectangle to avoid any ambiguity with drawing rectangles.
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 250, 550);

            // Create a push button field on the first page
            ButtonField submitButton = new ButtonField(doc.Pages[1], btnRect)
            {
                Name = "SubmitBtn",
                PartialName = "SubmitBtn",
                NormalCaption = "Submit",
                // Optional visual styling
                Color = Aspose.Pdf.Color.LightGray
            };

            // JavaScript to submit the form data to the specified endpoint
            string js = $"this.submitForm({{cURL:'{submitUrl}', cSubmitAs:'HTML'}});";
            JavascriptAction jsAction = new JavascriptAction(js);

            // Assign the JavaScript action to the button's mouse‑up (release) event
            submitButton.Actions.OnReleaseMouseBtn = jsAction;

            // Add the button to the document form collection (no need to add it to page annotations separately)
            doc.Form.Add(submitButton);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPdf}'.");
    }
}
