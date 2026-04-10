using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // SubmitFormAction
using Aspose.Pdf.Forms;      // ButtonField

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

        // Load the PDF (lifecycle rule: use using for disposal)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document has a form object
            Form form = doc.Form;

            // Define the button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button on the document (constructor: Document, Rectangle)
            ButtonField submitBtn = new ButtonField(doc, btnRect)
            {
                PartialName      = "SubmitBtn",
                NormalCaption    = "Submit",
                AlternateCaption = "Submit"
            };

            // Create the submit-form action and set the target URL
            SubmitFormAction submitAction = new SubmitFormAction
            {
                // Url property expects a FileSpecification, not a raw string
                Url = new FileSpecification(submitUrl)
                // Optional: submit as HTML form
                // Flags = SubmitFormAction.ExportFormat
            };

            // Assign the action to be executed when the button is activated
            submitBtn.OnActivated = submitAction;

            // Add the button to the form (automatically placed on the page)
            form.Add(submitBtn);

            // Save the modified PDF (lifecycle rule: save inside using)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}
