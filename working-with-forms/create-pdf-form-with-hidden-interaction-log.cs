using System;
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
            // Add a single page (1‑based indexing)
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Create a visible text box field for user input
            // -------------------------------------------------
            Aspose.Pdf.Rectangle nameRect = new Aspose.Pdf.Rectangle(100, 600, 300, 620);
            TextBoxField nameField = new TextBoxField(page, nameRect)
            {
                PartialName = "NameField",
                Contents   = "Enter name"
            };
            // Add the field to the document form
            doc.Form.Add(nameField);

            // -------------------------------------------------
            // 2. Create a hidden log field to store interaction logs
            // -------------------------------------------------
            // Place the hidden field off‑page (size zero) and set the Hidden flag
            Aspose.Pdf.Rectangle logRect = new Aspose.Pdf.Rectangle(0, 0, 0, 0);
            TextBoxField logField = new TextBoxField(page, logRect)
            {
                PartialName = "LogField",
                Flags       = AnnotationFlags.Hidden   // makes the field invisible in the viewer
            };
            doc.Form.Add(logField);

            // -------------------------------------------------
            // 3. Attach JavaScript to log interactions
            // -------------------------------------------------
            // When the user leaves the NameField, append a line to the hidden log field
            string js = @"
                var log = this.getField('LogField');
                if (log) {
                    log.value += 'NameField changed at ' + new Date().toISOString() + '\n';
                }
            ";
            // Use OnExit (or OnLostFocus) – OnBlur is not a valid action property
            nameField.Actions.OnExit = new JavascriptAction(js);

            // -------------------------------------------------
            // 4. Save the PDF document
            // -------------------------------------------------
            doc.Save("FormWithAnalytics.pdf");
        }

        Console.WriteLine("PDF form with hidden analytics log created successfully.");
    }
}
