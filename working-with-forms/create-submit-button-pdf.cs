using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "submit_form.pdf";

        // Ensure the Document is disposed properly
        using (Document doc = new Document())
        {
            // Add a page that will contain the button
            Page page = doc.Pages.Add();

            // Define the button rectangle (lower‑left X/Y, upper‑right X/Y)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 600, 200, 650);

            // Create a button field (ButtonField is the correct type for a push button)
            ButtonField submitBtn = new ButtonField(page, buttonRect);
            submitBtn.PartialName = "SubmitForm";   // field name
            submitBtn.Value = "Submit";             // button label

            // Create a submit action that posts the form data to the desired URL
            SubmitFormAction submitAction = new SubmitFormAction();
            submitAction.Url = new FileSpecification("https://api.example.com/submit");

            // Assign the action to the button – OnActivated is the proper trigger for a push button
            submitBtn.OnActivated = submitAction;

            // Add the button to the document's form collection
            doc.Form.Add(submitBtn, page.Number);

            // Save the PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}
