using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string submitUrl = "https://example.com/submit";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Access (or create) the AcroForm
            Form form = doc.Form;

            // Define the button rectangle (left, bottom, right, top)
            Rectangle btnRect = new Rectangle(100, 500, 200, 540);

            // Create a push‑button field
            ButtonField submitBtn = new ButtonField(doc, btnRect)
            {
                PartialName      = "SubmitBtn",
                NormalCaption    = "Submit",
                AlternateCaption = "Submit"
            };

            // SubmitFormAction expects a FileSpecification for the URL
            SubmitFormAction submitAction = new SubmitFormAction
            {
                Url   = new FileSpecification(submitUrl, "Submit URL"),
                Flags = SubmitFormAction.ExportFormat // export as HTML form (GET)
            };

            // Assign the action to a supported button event (press mouse button)
            submitBtn.Actions.OnPressMouseBtn = submitAction;

            // Add the button to page 1 of the document
            form.Add(submitBtn, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}
