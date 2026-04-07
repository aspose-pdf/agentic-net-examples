using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string buttonName = "SubmitForm";
        const string submitUrl = "https://api.example.com/submit";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF (wrap in using for deterministic disposal)
        using (Document doc = new Document(inputPath))
        {
            // Define the button rectangle (left, bottom, right, top)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button field on the document
            ButtonField submitButton = new ButtonField(doc, rect)
            {
                PartialName = buttonName,          // field name
                NormalCaption = "Submit",          // text shown on the button
                Color = Aspose.Pdf.Color.LightGray // optional visual styling
            };

            // Create the submit‑form action. The Url property expects a FileSpecification,
            // not a plain string, so we construct one with the target URL.
            SubmitFormAction submitAction = new SubmitFormAction
            {
                Url = new FileSpecification(submitUrl, "Submit Form")
                // Additional flags (e.g., ExportFormat, SubmitPdf) can be set via the Flags property if needed
            };

            // Attach the action to the button. AnnotationActionCollection does not expose an Add method;
            // instead, assign the action to the appropriate event property (e.g., OnReleaseMouseBtn).
            submitButton.Actions.OnReleaseMouseBtn = submitAction;

            // Add the button to the document's form collection
            doc.Form.Add(submitButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}
