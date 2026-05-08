using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string outputPdf = "form_with_backup.pdf";
        const string backupPdf = "backup_copy.pdf";

        // Create a new PDF document and add a single page.
        using (Document doc = new Document())
        {
            // Add a blank page (page indexing is 1‑based).
            doc.Pages.Add();

            // Create a text box field. Rectangle constructor: (llx, lly, urx, ury).
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);
            TextBoxField nameField = new TextBoxField(doc, fieldRect)
            {
                PartialName = "NameField",
                Value = "" // initial empty value
            };

            // Add the field to the form on page 1.
            doc.Form.Add(nameField, 1);

            // Attach a JavaScript action that saves a backup copy each time the field value changes.
            // Use a valid action property (OnCalculate) for value‑change events.
            nameField.Actions.OnCalculate = new JavascriptAction($"this.saveAs('{backupPdf}');");

            // Optional: ensure that calculated fields are recomputed after any change.
            doc.Form.AutoRecalculate = true;

            // Save the PDF containing the form.
            doc.Save(outputPdf);
        }

        Console.WriteLine($"Form PDF saved to '{outputPdf}'. A backup will be created as '{backupPdf}' after each field modification when opened in a PDF viewer that supports JavaScript.");
    }
}
