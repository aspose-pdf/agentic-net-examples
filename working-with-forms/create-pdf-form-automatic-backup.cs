using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPdf = "form_with_backup.pdf";
        const string backupPdf = "backup_copy.pdf";

        // Create a new PDF document and add a blank page.
        using (Document doc = new Document())
        {
            doc.Pages.Add();

            // Initialize FormEditor with the document.
            FormEditor formEditor = new FormEditor(doc);

            // Add a text field to the first page.
            // Parameters: field type, field name, page number, llx, lly, urx, ury
            formEditor.AddField(FieldType.Text, "SampleField", 1, 100f, 700f, 300f, 720f);

            // JavaScript to save a backup copy whenever the field value changes.
            // The script uses the PDF JavaScript 'this.saveAs' method.
            string jsScript = $"this.saveAs('{backupPdf}');";

            // Attach the script to the field. This script runs on the field's "On Blur" event.
            formEditor.AddFieldScript("SampleField", jsScript);

            // Save the PDF with the form.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Form PDF saved to '{outputPdf}'. Backup will be created as '{backupPdf}' after each field modification.");
    }
}