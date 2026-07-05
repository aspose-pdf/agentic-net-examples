using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the button
            Page page = doc.Pages.Add();

            // Define the button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button field on the page
            ButtonField submitButton = new ButtonField(page, buttonRect)
            {
                // Internal name of the field
                PartialName = "SubmitButton",

                // Tooltip shown in Acrobat (alternate name)
                AlternateName = "Please fill all required fields before submitting.",

                // Mark the field as required (optional)
                Required = true
            };

            // Configure the submit action (POST to a URL)
            SubmitFormAction submitAction = new SubmitFormAction
            {
                // Url property expects a FileSpecification, not a plain string
                Url = new FileSpecification("https://example.com/submit")
            };

            // Attach the submit action to the button – use a supported action property
            // OnReleaseMouseBtn is the correct property for a button click
            submitButton.Actions.OnReleaseMouseBtn = submitAction;

            // Add the button to the document's form on page 1 (1‑based indexing)
            doc.Form.Add(submitButton, 1);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}
