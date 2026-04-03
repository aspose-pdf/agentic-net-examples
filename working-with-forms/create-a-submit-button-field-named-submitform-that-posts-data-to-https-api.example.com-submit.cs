using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "SubmitForm.pdf";

        // Create a new PDF document with a single page
        using (Document doc = new Document())
        {
            // Add a blank page (Pages are 1‑based)
            doc.Pages.Add();

            // Initialize FormEditor with the document
            using (FormEditor formEditor = new FormEditor(doc))
            {
                // Add a submit button named "SubmitForm" on page 1.
                // Parameters: fieldName, pageNumber, buttonLabel, submitUrl,
                // llx, lly, urx, ury (coordinates in points)
                formEditor.AddSubmitBtn(
                    fieldName: "SubmitForm",
                    page: 1,
                    label: "Submit",
                    url: "https://api.example.com/submit",
                    llx: 100f,
                    lly: 700f,
                    urx: 200f,
                    ury: 750f);

                // Save the modified document
                formEditor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with submit button saved to '{outputPath}'.");
    }
}