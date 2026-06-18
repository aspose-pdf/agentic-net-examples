using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations; // for JavascriptAction

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (required before adding form fields)
            Page page = doc.Pages.Add();

            // Define the rectangle where the text box will appear
            // Use Aspose.Pdf.Rectangle (not Aspose.Pdf.Drawing.Rectangle)
            Aspose.Pdf.Rectangle rect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);

            // Create a text box field
            TextBoxField txtField = new TextBoxField(page, rect)
            {
                PartialName = "SampleField",
                Value = "" // initial empty value
            };

            // Attach JavaScript that saves a backup copy whenever the field value changes
            // Use a valid action property (OnModifyCharacter) from AnnotationActionCollection
            txtField.Actions.OnModifyCharacter = new JavascriptAction("this.saveAs('backup_copy.pdf');");

            // Add the field to the document's form
            doc.Form.Add(txtField);

            // Optional: ensure calculated fields are updated automatically
            doc.Form.AutoRecalculate = true;

            // Save the PDF form
            doc.Save("interactive_form.pdf");
        }

        Console.WriteLine("PDF form created. Modifying the field will trigger a backup save as 'backup_copy.pdf'.");
    }
}