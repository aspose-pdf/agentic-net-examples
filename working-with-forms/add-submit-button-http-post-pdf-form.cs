using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string submitUrl = "https://example.com/submit";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            // Define button rectangle (avoid System.Drawing.Rectangle ambiguity)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Configure the submit action. SubmitFormAction only exposes the Url property.
            // The default submit method is POST and the default format is URL‑encoded.
            SubmitFormAction submitAction = new SubmitFormAction();
            submitAction.Url = new FileSpecification(submitUrl);

            // Create the button field and assign the action.
            ButtonField submitBtn = new ButtonField(doc.Pages[1], rect)
            {
                PartialName = "SubmitBtn",
                NormalCaption = "Submit",
                OnActivated = submitAction
            };

            // Add the button to the document's form collection.
            doc.Form.Add(submitBtn);

            // Save the modified PDF.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with configured submit button saved to '{outputPdf}'.");
    }
}
