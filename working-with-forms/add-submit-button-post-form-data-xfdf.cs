using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";          // PDF containing the form
        const string outputPdf = "output_with_submit.pdf"; // PDF after adding submit action
        const string remoteUrl = "https://example.com/receiveFormData"; // Remote XML endpoint

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPdf))
        {
            // Create a submit button on the first page
            Rectangle btnRect = new Rectangle(100, 500, 200, 550);
            ButtonField submitButton = new ButtonField(doc.Pages[1], btnRect)
            {
                // Use PartialName instead of Name
                PartialName = "SubmitBtn",
                // Use NormalCaption to set the button label
                NormalCaption = "Submit"
            };

            // Configure the submit action
            SubmitFormAction submitAction = new SubmitFormAction
            {
                // Url expects a FileSpecification, not a raw string
                Url = new FileSpecification(remoteUrl),
                // Submit the form data as XFDF (XML‑based FDF)
                Flags = SubmitFormAction.Xfdf
            };

            // Assign the action to the button (ButtonField has no Action property)
            submitButton.OnActivated = submitAction;

            // Add the button to the form
            doc.Form.Add(submitButton);

            // Save the modified PDF
            doc.Save(outputPdf);
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPdf}'.");
    }
}
