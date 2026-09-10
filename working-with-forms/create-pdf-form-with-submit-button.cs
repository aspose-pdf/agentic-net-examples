using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class CreatePdfFormWithSubmit
{
    static void Main()
    {
        const string outputPath = "FormWithSubmit.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Add a text box field for user input
            // -------------------------------------------------
            var txtRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);
            var txtField = new TextBoxField(page, txtRect)
            {
                PartialName = "UserName",
                Value = ""
            };
            doc.Form.Add(txtField);

            // -------------------------------------------------
            // 2. Add a button that will submit the form
            // -------------------------------------------------
            var btnRect = new Aspose.Pdf.Rectangle(100, 560, 200, 580);
            var submitBtn = new ButtonField(page, btnRect)
            {
                PartialName = "SubmitBtn",
                Value = "Submit"
            };

            var submitAction = new SubmitFormAction
            {
                // Url property expects a FileSpecification, not a plain string
                Url = new FileSpecification("https://example.com/api/submit")
                // Flags can be set here, e.g. Flags = SubmitFormAction.SubmitPdf;
            };

            // Attach the submit action to the button's mouse‑press event (valid property)
            submitBtn.Actions.OnPressMouseBtn = submitAction;

            doc.Form.Add(submitBtn);

            // -------------------------------------------------
            // 3. Save the PDF document
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with submit action saved to '{outputPath}'.");
    }
}
