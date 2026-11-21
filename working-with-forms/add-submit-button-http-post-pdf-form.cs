using System;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_submit.pdf";

        // Create a new PDF document inside a using block for proper disposal
        using (Document doc = new Document())
        {
            // Add a blank page (Pages are 1‑based)
            Page page = doc.Pages.Add();

            // Create a submit button field
            ButtonField submitBtn = new ButtonField(
                page,
                new Aspose.Pdf.Rectangle(100, 600, 200, 650) // fully qualified to avoid ambiguity
            )
            {
                // Required name for the field
                PartialName = "submitBtn",
                // Define appearance to avoid NullReferenceException when setting caption
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black),
                // Set the visible label on the button
                NormalCaption = "Submit"
            };

            // Configure the action: HTTP POST with URL‑encoded form data
            SubmitFormAction submitAction = new SubmitFormAction();
            submitAction.Url = new FileSpecification("https://example.com/submit");
            // ExportFormat posts the form fields as URL‑encoded data via HTTP POST
            submitAction.Flags = SubmitFormAction.ExportFormat;
            submitBtn.OnActivated = submitAction;

            // Add the button to the document's AcroForm (field collection)
            doc.Form.Add(submitBtn, 1);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}
