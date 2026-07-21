using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";          // existing PDF with form fields
        const string outputPath = "output_with_submit.pdf";
        const string submitUrl = "https://example.com/submit"; // endpoint to receive form data

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a form (creates one if absent)
            Form form = doc.Form;

            // Define button rectangle (llx, lly, urx, ury)
            var btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button field on the first page
            var submitButton = new ButtonField(doc.Pages[1], btnRect)
            {
                PartialName = "SubmitButton",          // internal field name
                NormalCaption = "Submit",              // text shown on the button
                Color = Aspose.Pdf.Color.LightGray     // optional visual styling
            };

            // Create a SubmitFormAction and set the target URL using FileSpecification
            var submitAction = new SubmitFormAction
            {
                Url = new FileSpecification(submitUrl)
                // Optional: set ExportFormat, e.g., submitAction.ExportFormat = SubmitFormAction.ExportFormatEnum.FDF;
            };

            // Attach the action to the button's mouse‑up (release) event using the correct property
            submitButton.Actions.OnReleaseMouseBtn = submitAction;

            // Add the button to the form on page 1
            form.Add(submitButton, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}
