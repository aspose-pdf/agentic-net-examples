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
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the button rectangle (lower‑left and upper‑right coordinates)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 700, 200, 730);

            // Create a push button field on page 1
            ButtonField submitBtn = new ButtonField(doc.Pages[1], btnRect)
            {
                PartialName   = "submitBtn",
                NormalCaption = "Submit"
            };

            // Configure the submit action:
            // - ExportFormat flag makes the data be sent as URL‑encoded HTML form (POST)
            // - Do NOT set GetMethod flag (which would switch to HTTP GET)
            SubmitFormAction action = new SubmitFormAction
            {
                Url   = new FileSpecification(submitUrl),
                Flags = SubmitFormAction.ExportFormat   // POST with URL‑encoded form data
            };

            // Assign the action to the button's activation event
            submitBtn.OnActivated = action;

            // Add the button to the document's form collection
            doc.Form.Add(submitBtn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with POST submit button saved to '{outputPath}'.");
    }
}