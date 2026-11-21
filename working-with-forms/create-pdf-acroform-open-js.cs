using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "AcroFormWithJS.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to the document
            Page page = doc.Pages.Add();

            // Create a text box field (AcroForm) on the page
            // Constructor takes (Page, Rectangle). The field name is set via PartialName.
            var textRect = new Rectangle(100, 700, 300, 720);
            TextBoxField nameField = new TextBoxField(page, textRect);
            nameField.PartialName = "NameField";
            nameField.Value = "Enter name";
            doc.Form.Add(nameField);

            // Create a checkbox field (AcroForm) on the page
            // Correct class name is "CheckboxField" (lower‑case 'b') and uses the same constructor pattern.
            var checkRect = new Rectangle(100, 650, 120, 670);
            CheckboxField agreeField = new CheckboxField(page, checkRect);
            agreeField.PartialName = "AgreeCheck";
            agreeField.Checked = false;
            doc.Form.Add(agreeField);

            // Set a document‑level JavaScript that runs when the PDF is opened
            // The class name is "JavascriptAction" (lower‑case 's') in Aspose.Pdf.Annotations.
            doc.OpenAction = new JavascriptAction("app.alert('Document opened');");

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
