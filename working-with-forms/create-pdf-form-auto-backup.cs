using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "FormWithBackup.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the rectangle for the text box field (fully qualified to avoid ambiguity)
            Aspose.Pdf.Rectangle fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);

            // Create a text box field and add it to the document's form
            TextBoxField txtField = new TextBoxField(page, fieldRect)
            {
                PartialName = "SampleField",
                Value = "Enter text here"
            };

            // Attach a JavaScript action that saves a backup copy whenever the field value changes
            // Use the valid OnModifyCharacter action (value‑change equivalent) for form fields
            txtField.Actions.OnModifyCharacter = new JavascriptAction("this.saveAs('BackupCopy.pdf');");

            // Add the field to the form collection
            doc.Form.Add(txtField);

            // Save the PDF (the JavaScript will handle backup creation at runtime)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form created: {outputPath}");
    }
}