using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // FormEditor, FieldType

class Program
{
    static void Main()
    {
        const string outputPath = "MultiPageForm.pdf";
        const int pageCount = 3;               // number of pages to create
        const float llx = 100f;                 // lower‑left X of the field
        const float lly = 500f;                 // lower‑left Y of the field
        const float urx = 300f;                 // upper‑right X of the field
        const float ury = 520f;                 // upper‑right Y of the field

        // Create a new PDF document and add the required pages.
        using (Document doc = new Document())
        {
            for (int i = 0; i < pageCount; i++)
                doc.Pages.Add();                // 1‑based indexing; pages are added sequentially

            // FormEditor works on the document and allows adding form fields.
            using (FormEditor editor = new FormEditor(doc))
            {
                // Add a set of fields to each page. Each field gets a unique name.
                for (int pageNum = 1; pageNum <= pageCount; pageNum++)
                {
                    string fieldName = $"TextField_{pageNum}";   // unique name per page
                    // Add a text box field to the current page.
                    editor.AddField(FieldType.Text, fieldName, pageNum, llx, lly, urx, ury);
                }

                // Persist the changes. FormEditor.Save(string) is the correct method.
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"PDF with {pageCount} pages created: {outputPath}");
    }
}