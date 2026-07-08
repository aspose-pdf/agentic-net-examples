using System;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_backup.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Define the rectangle for the text box field (left, bottom, right, top)
            var fieldRect = new Aspose.Pdf.Rectangle(100, 600, 300, 630);

            // Create a text box field using the (Page, Rectangle) constructor
            TextBoxField textField = new TextBoxField(page, fieldRect)
            {
                PartialName = "SampleField",
                Value = "Enter text"
            };

            // Attach JavaScript that saves a backup copy whenever the field value changes.
            // OnModifyCharacter is the correct trigger for character‑level modifications.
            textField.Actions.OnModifyCharacter = new JavascriptAction("this.saveAs('backup.pdf');");

            // Add the field to the form on page 1
            doc.Form.Add(textField, 1);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with automatic backup saved to '{outputPath}'.");
    }
}