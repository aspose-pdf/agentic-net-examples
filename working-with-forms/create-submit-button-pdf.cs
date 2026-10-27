using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "SubmitForm.pdf";

        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);

            ButtonField submitButton = new ButtonField(page, buttonRect)
            {
                PartialName = "SubmitForm",
                Value = "Submit"
            };

            // Create a SubmitFormAction, set its Url, and assign it to a supported action property.
            SubmitFormAction submitAction = new SubmitFormAction();
            submitAction.Url = new FileSpecification("https://api.example.com/submit");

            // Use a valid action property (OnPressMouseBtn) to trigger the submit.
            submitButton.Actions.OnPressMouseBtn = submitAction;

            doc.Form.Add(submitButton, 1);

            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}