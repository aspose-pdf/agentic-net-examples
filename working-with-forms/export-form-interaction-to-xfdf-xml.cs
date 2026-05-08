using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";
        const string outputXfdf = "submission.xfdf";
        const string outputXml = "document_model.xml";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document(inputPdf))
        {
            // Locate a button field that will act as the submit button.
            ButtonField submitButton = null;
            if (doc.Form != null && doc.Form.Fields != null)
            {
                foreach (Field field in doc.Form.Fields)
                {
                    if (field is ButtonField btn)
                    {
                        submitButton = btn;
                        break;
                    }
                }
            }

            if (submitButton != null)
            {
                // Configure a SubmitFormAction that posts the form data as XFDF (XML).
                var submitAction = new SubmitFormAction
                {
                    // Url property expects a FileSpecification, not a raw string.
                    Url = new FileSpecification("https://example.com/receive"),
                    Flags = SubmitFormAction.Xfdf // Export data in XFDF format.
                };
                // Assign the action to the button's mouse‑up (release) event using a valid property.
                submitButton.Actions.OnReleaseMouseBtn = submitAction;
            }

            // Export the form interaction (field values, annotations) to XFDF.
            // The ExportAnnotationsToXfdf overload that accepts a file path (string) is used.
            doc.ExportAnnotationsToXfdf(outputXfdf);

            // Optionally, save the entire PDF document model as XML for diagnostics.
            doc.SaveXml(outputXml);
        }

        Console.WriteLine("Interaction log exported to XFDF and document model saved as XML.");
    }
}
