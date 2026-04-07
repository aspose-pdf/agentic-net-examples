using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // for FieldType and SubmitFormFlag

class Program
{
    static void Main()
    {
        const string outputPath = "SubmitButton.pdf";
        const string submitUrl   = "https://example.com/submit";

        // Create a new PDF document and add a blank page
        using (Document doc = new Document())
        {
            doc.Pages.Add();

            // Use FormEditor to add a push‑button that submits the form
            using (FormEditor formEditor = new FormEditor())
            {
                // Bind the editor to the in‑memory document
                formEditor.BindPdf(doc);

                // Define button properties
                string buttonName = "btnSubmit";
                int    pageNumber = 1;                     // 1‑based page index
                float  llx = 100f, lly = 700f, urx = 200f, ury = 750f;
                string buttonLabel = "Submit";

                // Add a submit button that posts to the specified URL
                formEditor.AddSubmitBtn(buttonName, pageNumber, buttonLabel, submitUrl,
                                        llx, lly, urx, ury);

                // Additionally attach JavaScript that performs the same submit action
                // (optional – demonstrates JavaScript usage)
                string js = $"this.submitForm({{cURL:'{submitUrl}'}});";
                formEditor.SetFieldScript(buttonName, js);

                // Save the modified PDF
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}