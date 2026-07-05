using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";
        const string submitUrl = "https://example.com/submit";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        using (Document doc = new Document(inputPdf))
        {
            // Define button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create the button field
            ButtonField submitBtn = new ButtonField(doc.Pages[1], btnRect)
            {
                Color = Aspose.Pdf.Color.LightGray,
                NormalCaption = "Submit"
            };

            // Border must be set after the button instance exists (requires parent annotation)
            submitBtn.Border = new Border(submitBtn) { Width = 1 };

            // Create SubmitFormAction and assign the URL using a FileSpecification instance
            SubmitFormAction submitAction = new SubmitFormAction
            {
                Url = new FileSpecification(submitUrl)
            };

            // Attach the action to the button's activation event
            submitBtn.OnActivated = submitAction;

            // Add the button to the page annotations collection
            doc.Pages[1].Annotations.Add(submitBtn);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Submit button configured and saved to '{outputPdf}'.");
    }
}
