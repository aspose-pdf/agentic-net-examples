using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string submitUrl  = "https://example.com/submit";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (page indexing is 1‑based)
            Page page = doc.Pages[1];

            // Define the button rectangle (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button field on the page
            ButtonField submitButton = new ButtonField(page, rect)
            {
                // Set a name for the field (used as the form field name)
                PartialName = "SubmitBtn",
                // Caption shown on the button
                NormalCaption = "Submit"
            };

            // JavaScript that submits the form data to the specified HTTP endpoint
            string jsCode = $"this.submitForm({{cURL:'{submitUrl}'}});";
            JavascriptAction jsAction = new JavascriptAction(jsCode);

            // Assign the JavaScript action to the button's activation event
            submitButton.OnActivated = jsAction;

            // Add the button to the page annotations collection
            page.Annotations.Add(submitButton);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}