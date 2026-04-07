using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "MultiPageForm.pdf";

        // Create a new PDF document and ensure it is disposed properly.
        using (Document doc = new Document())
        {
            // Define how many pages the document will have.
            int pageCount = 3;

            // Add blank pages (Aspose.Pdf uses 1‑based page indexing).
            for (int i = 0; i < pageCount; i++)
                doc.Pages.Add();

            // FormEditor works on an existing Document instance.
            FormEditor formEditor = new FormEditor(doc);

            // Define rectangles for the fields (coordinates are in points).
            // Text field rectangle.
            float txtLlx = 100f, txtLly = 600f, txtUrx = 300f, txtUry = 630f;
            // Check box rectangle.
            float chkLlx = 100f, chkLly = 550f, chkUrx = 120f, chkUry = 570f;

            // Add the same set of fields to each page, giving each a unique name.
            for (int pageNum = 1; pageNum <= pageCount; pageNum++)
            {
                // Unique name for the text field on this page.
                string textFieldName = $"TextField_Page{pageNum}";
                formEditor.AddField(FieldType.Text, textFieldName, pageNum,
                                    txtLlx, txtLly, txtUrx, txtUry);

                // Unique name for the check box on this page.
                string checkBoxName = $"CheckBox_Page{pageNum}";
                formEditor.AddField(FieldType.CheckBox, checkBoxName, pageNum,
                                    chkLlx, chkLly, chkUrx, chkUry);
            }

            // Persist the PDF with all cloned form fields.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Multi‑page PDF with cloned form fields saved to '{outputPath}'.");
    }
}