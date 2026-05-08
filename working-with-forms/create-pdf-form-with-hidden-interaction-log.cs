using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // ------------------------------------------------------------
            // Hidden log field (will store interaction logs)
            // Placed off‑page and marked as Hidden so it is not visible.
            // ------------------------------------------------------------
            TextBoxField logField = new TextBoxField(page, new Aspose.Pdf.Rectangle(0, 0, 0, 0));
            logField.PartialName = "Log";
            logField.Flags = AnnotationFlags.Hidden; // hide the field
            doc.Form.Add(logField);

            // ------------------------------------------------------------
            // Example visible form fields
            // ------------------------------------------------------------

            // Text box field for "Name"
            TextBoxField nameField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 600, 300, 620));
            nameField.PartialName = "Name";
            nameField.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            doc.Form.Add(nameField);

            // Checkbox field for "Subscribe"
            CheckboxField subscribeField = new CheckboxField(page, new Aspose.Pdf.Rectangle(100, 560, 120, 580));
            subscribeField.PartialName = "Subscribe";
            doc.Form.Add(subscribeField);

            // ------------------------------------------------------------
            // Attach JavaScript to log interactions.
            // The script appends a line to the hidden Log field whenever the
            // user leaves (OnExit) a form field.
            // ------------------------------------------------------------

            string jsTemplate = @"
var log = this.getField('Log');
log.value += '\nField {0} changed at ' + new Date().toISOString();";

            // Name field logging
            nameField.Actions.OnExit = new JavascriptAction(string.Format(jsTemplate, "Name"));

            // Subscribe checkbox logging
            subscribeField.Actions.OnExit = new JavascriptAction(string.Format(jsTemplate, "Subscribe"));

            // ------------------------------------------------------------
            // Save the PDF with the form and hidden log field.
            // ------------------------------------------------------------
            doc.Save("FormWithAnalytics.pdf");
        }

        Console.WriteLine("PDF form created with hidden analytics log field.");
    }
}
