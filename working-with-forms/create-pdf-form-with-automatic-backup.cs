using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_backup.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page to host the form field
            Page page = doc.Pages.Add();

            // Define the field rectangle (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a text box form field
            TextBoxField txtField = new TextBoxField(page, fieldRect)
            {
                PartialName = "SampleField",
                Value = ""
            };

            // JavaScript that saves a backup copy each time the field loses focus after a change
            string js = "var dt = new Date(); " +
                        "var name = 'backup_' + util.printd('yyyyMMddHHmmss', dt) + '.pdf'; " +
                        "this.saveAs(name);";

            // Use the correct action property for "blur" (loss of focus)
            txtField.Actions.OnLostFocus = new JavascriptAction(js);

            // Add the field to the document's form (page index is 1‑based)
            doc.Form.Add(txtField, 1);

            // Save the PDF form
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form created: {outputPath}");
    }
}
