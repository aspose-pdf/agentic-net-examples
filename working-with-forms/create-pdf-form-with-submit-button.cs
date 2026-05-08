using System;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class CreatePdfFormWithSubmit
{
    static void Main()
    {
        const string outputPath = "FormWithSubmit.pdf";
        const string submitUrl = "https://example.com/api/submit";

        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Add a text box field for user input
            // -------------------------------------------------
            Aspose.Pdf.Rectangle txtRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);
            TextBoxField txtField = new TextBoxField(page, txtRect)
            {
                // Set the field name
                PartialName = "UserName",
                // Optional: default appearance (font, size, color)
                DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black),
                // Optional: placeholder value
                Value = "Enter name here"
            };
            doc.Form.Add(txtField);

            // -------------------------------------------------
            // 2. Add a submit button that posts the form data
            // -------------------------------------------------
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 540, 200, 570);
            ButtonField submitBtn = new ButtonField(page, btnRect)
            {
                PartialName = "SubmitBtn",
                Value = "Submit"
            };

            // Configure the submit action – URL must be wrapped in a FileSpecification
            SubmitFormAction submitAction = new SubmitFormAction
            {
                Url = new FileSpecification(submitUrl)
                // Flags can be set here if a different export format is required
            };

            // Assign the submit action to the button's mouse‑up event (the only
            // supported property for a button click in Aspose.Pdf)
            submitBtn.Actions.OnReleaseMouseBtn = submitAction;

            doc.Form.Add(submitBtn);

            // -------------------------------------------------
            // Save the PDF document
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with submit button saved to '{outputPath}'.");
    }
}