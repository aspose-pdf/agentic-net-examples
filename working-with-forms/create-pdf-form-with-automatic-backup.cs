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

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (first page is at index 1)
            doc.Pages.Add();

            // Define the rectangle for the text box field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 700, 300, 730);

            // Create a text box form field on the first page
            TextBoxField nameField = new TextBoxField(doc.Pages[1], fieldRect)
            {
                PartialName = "NameField",   // field identifier
                Value = ""                   // initial empty value
            };

            // Attach JavaScript that saves a backup copy whenever the field value changes.
            // Use the valid OnModifyCharacter action (fires on each character change).
            nameField.Actions.OnModifyCharacter = new JavascriptAction(
                "this.saveAs('backup_' + this.documentFileName);"
            );

            // Add the field to the document's form collection
            doc.Form.Add(nameField);

            // Save the PDF form
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form created: {Path.GetFullPath(outputPath)}");
    }
}
