using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_submit.pdf";
        const string submitUrl = "https://example.com/submit";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Define the button rectangle (use Aspose.Pdf.Rectangle for form field bounds)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button field on the first page
            ButtonField submitButton = new ButtonField(doc.Pages[1], btnRect)
            {
                Name = "SubmitButton",
                AlternateCaption = "Submit"
            };

            // Ensure the button is part of the form
            doc.Form.Add(submitButton, 1);

            // Create a SubmitFormAction that posts the form data to the specified URL
            SubmitFormAction submitAction = new SubmitFormAction
            {
                // Url property expects a FileSpecification instance
                Url = new FileSpecification(submitUrl, "Submit URL"),
                Flags = SubmitFormAction.ExportFormat // submit as HTML form
            };

            // Assign the action to the button's mouse‑press event (valid property name)
            submitButton.Actions.OnPressMouseBtn = submitAction;

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}
