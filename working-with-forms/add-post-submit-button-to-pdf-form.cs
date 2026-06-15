using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

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

        // Open the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Define the button rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 540);

            // Create a push button field on the first page
            ButtonField submitBtn = new ButtonField(doc.Pages[1], btnRect)
            {
                PartialName   = "submitBtn",   // internal field name
                NormalCaption = "Submit"       // visible label on the button
            };

            // Configure the submit action
            SubmitFormAction submitAction = new SubmitFormAction
            {
                // Destination URL for the form submission
                Url = new FileSpecification(submitUrl),

                // Use HTML form format (application/x-www-form-urlencoded) and POST method.
                // POST is the default; we only set the ExportFormat flag.
                Flags = SubmitFormAction.ExportFormat
            };

            // Assign the action to the button's activation event
            submitBtn.OnActivated = submitAction;

            // Add the button to the document's form collection
            doc.Form.Add(submitBtn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with POST submit button saved to '{outputPath}'.");
    }
}