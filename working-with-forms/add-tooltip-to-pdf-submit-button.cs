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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Define button rectangle (Aspose.Pdf.Rectangle, not System.Drawing.Rectangle)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 540);

            // Create a push‑button field
            ButtonField submitBtn = new ButtonField(doc, btnRect)
            {
                PartialName = "SubmitBtn",
                // Tooltip property is not available in this API version – use AlternateName as a fallback
                AlternateName = "All required fields must be filled before submitting.",
                Contents = "Submit",
                Required = true
            };

            // SubmitFormAction expects a FileSpecification for the URL in this version
            var urlSpec = new FileSpecification("https://example.com/submit", "Submit URL");
            SubmitFormAction submitAction = new SubmitFormAction
            {
                Url = urlSpec
            };

            // Attach the action to the button's mouse‑press event
            submitBtn.Actions.OnPressMouseBtn = submitAction;

            // Add the button to page 1 of the document
            doc.Form.Add(submitBtn, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with tooltip to '{outputPath}'.");
    }
}
