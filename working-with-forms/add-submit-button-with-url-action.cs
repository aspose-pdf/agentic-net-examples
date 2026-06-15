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
        const string submitUrl = "https://example.com/submit";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the existing PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button field on the document
            ButtonField submitButton = new ButtonField(doc, btnRect)
            {
                PartialName = "SubmitButton",          // internal field name
                AlternateCaption = "Submitting...",    // caption when pressed
                NormalCaption = "Submit"               // default caption
            };

            // Create a SubmitFormAction and set its destination URL using FileSpecification
            SubmitFormAction submitAction = new SubmitFormAction
            {
                Url = new FileSpecification(submitUrl)
            };

            // Assign the action to the button's mouse‑press event
            submitButton.Actions.OnPressMouseBtn = submitAction;

            // Add the button field to the document's form collection
            doc.Form.Add(submitButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with submit button: {outputPath}");
    }
}
