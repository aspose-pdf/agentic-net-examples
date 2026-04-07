using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // FormEditor, FieldType, SubmitFormFlag

class Program
{
    static void Main()
    {
        const string outputPath = "FormWithSubmit.pdf";
        const string submitUrl  = "https://example.com/api/submit";

        // Create a new PDF document and add a single page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Initialize FormEditor with the created document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a text field named "Name" (page 1, rectangle coordinates)
                formEditor.AddField(FieldType.Text, "Name", 1, 50, 700, 250, 730);

                // Add a submit button that will post the form data to the REST endpoint
                formEditor.AddSubmitBtn(
                    fieldName: "SubmitBtn",
                    page: 1,
                    label: "Submit",
                    url: submitUrl,
                    llx: 300,   // lower‑left X
                    lly: 700,   // lower‑left Y
                    urx: 380,   // upper‑right X
                    ury: 730);  // upper‑right Y

                // Configure the button to submit the whole PDF (optional, can use Html, Fdf, etc.)
                formEditor.SetSubmitFlag("SubmitBtn", SubmitFormFlag.Pdf);

                // Save the PDF with the form and submit button
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF form with submit button saved to '{outputPath}'.");
    }
}