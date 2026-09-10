using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "form.pdf";               // source PDF with form fields
        const string outputPdf = "form_with_submit.pdf";  // PDF after adding submit button
        const string interactionLog = "interaction_log.xfdf"; // XFDF (XML) log file

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // ------------------------------------------------------------
        // 1. Load the PDF and add a submit button that posts XFDF data.
        // ------------------------------------------------------------
        using (Document doc = new Document(inputPdf))
        {
            // Choose the page where the button will be placed (first page)
            Page page = doc.Pages[1];

            // Define button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a button field and configure its appearance
            ButtonField submitBtn = new ButtonField(page, rect)
            {
                PartialName = "SubmitBtn",
                NormalCaption = "Submit"
            };

            // Create a SubmitFormAction that sends data as XFDF (XML)
            SubmitFormAction submitAction = new SubmitFormAction
            {
                Url = new FileSpecification("https://example.com/receive"), // server endpoint
                Flags = SubmitFormAction.Xfdf        // export as XFDF (XML)
            };

            // Attach the action to the button (use OnActivated for button fields)
            submitBtn.OnActivated = submitAction;

            // Add the button to the document's form collection
            doc.Form.Add(submitBtn);

            // Save the modified PDF (contains the submit button)
            doc.Save(outputPdf);
        }

        // ------------------------------------------------------------
        // 2. After the form is submitted, export the interaction log.
        //    ExportAnnotationsToXfdf writes an XML‑based XFDF file that
        //    contains field names, values, and annotation data.
        // ------------------------------------------------------------
        using (Document doc = new Document(outputPdf))
        {
            // Export all annotations (including the submit action) to XFDF
            doc.ExportAnnotationsToXfdf(interactionLog);
        }

        Console.WriteLine("Form prepared and interaction log exported to XML (XFDF).");
    }
}
