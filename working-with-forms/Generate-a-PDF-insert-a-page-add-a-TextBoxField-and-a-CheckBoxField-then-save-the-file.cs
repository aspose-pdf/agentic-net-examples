using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string outputPath = "output.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Insert a blank page (pages are 1‑based)
            doc.Pages.Add();

            // Initialize FormEditor with the document instance
            FormEditor formEditor = new FormEditor(doc);

            // Add a text box field on page 1
            // Parameters: field type, field name, page number, llx, lly, urx, ury
            formEditor.AddField(FieldType.Text, "MyTextBox", 1, 100, 500, 300, 530);

            // Add a check box field on page 1
            formEditor.AddField(FieldType.CheckBox, "MyCheckBox", 1, 100, 450, 120, 470);

            // Persist the changes – use the non‑obsolete Save(string) overload
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}