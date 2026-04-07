using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_log.pdf";

        // Create a new PDF document and ensure deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page to host the form fields
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Visible text box field where the user enters data
            // -------------------------------------------------
            // Rectangle coordinates: left, bottom, right, top
            Aspose.Pdf.Rectangle nameRect = new Aspose.Pdf.Rectangle(100, 700, 300, 720);
            TextBoxField nameField = new TextBoxField(doc, nameRect)
            {
                PartialName = "Name",          // Field identifier
                Contents = "Enter name"        // Tooltip / default text
            };

            // -------------------------------------------------
            // Hidden log field that will collect interaction data
            // -------------------------------------------------
            // Zero‑size rectangle keeps the field invisible
            Aspose.Pdf.Rectangle logRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextBoxField logField = new TextBoxField(doc, logRect)
            {
                PartialName = "Log",
                ReadOnly = true                // Prevent user editing
            };

            // -------------------------------------------------
            // JavaScript that appends a log entry when the field is activated
            // -------------------------------------------------
            // The script retrieves the hidden log field and appends a line.
            string jsCode = "var log = this.getField('Log'); " +
                            "if (log) { log.value += '\\n' + 'Name field activated'; }";
            JavascriptAction jsAction = new JavascriptAction(jsCode);
            nameField.OnActivated = jsAction;

            // Add the fields to the form on page 1
            doc.Form.Add(nameField, 1);
            doc.Form.Add(logField, 1);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with hidden log saved to '{outputPath}'.");
    }
}