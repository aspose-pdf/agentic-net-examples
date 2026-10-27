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
        const string outputPath = "form_with_logging.pdf";

        // Document lifecycle must be wrapped in a using block
        using (Document doc = new Document())
        {
            // Add a single page to host the form fields
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Hidden log field (stores interaction logs)
            // -------------------------------------------------
            TextBoxField logField = new TextBoxField(page, new Aspose.Pdf.Rectangle(0, 0, 0, 0));
            logField.PartialName = "log";
            logField.Flags = AnnotationFlags.Hidden; // make the field invisible
            logField.Multiline = true; // allow multiple lines of text
            doc.Form.Add(logField);

            // -------------------------------------------------
            // Helper to create a visible text field with logging
            // -------------------------------------------------
            void AddLoggingTextField(string fieldName, string label, double llx, double lly, double urx, double ury)
            {
                // Visible text box field
                TextBoxField txtField = new TextBoxField(page, new Aspose.Pdf.Rectangle(llx, lly, urx, ury));
                txtField.PartialName = fieldName;
                txtField.Value = "";
                doc.Form.Add(txtField);

                // Optional label placed above the field
                TextFragment lbl = new TextFragment(label);
                lbl.Position = new Position(llx, ury + 5);
                page.Paragraphs.Add(lbl);

                // JavaScript that appends an entry to the hidden log field
                string js = $@"var log = this.getField('log');
if (log) {{
    log.value += '{fieldName} interacted at ' + new Date().toISOString() + '\r\n';
}}";

                // Attach the script to common interaction events
                JavascriptAction jsAction = new JavascriptAction(js);
                txtField.Actions.OnEnter = jsAction;          // focus
                txtField.Actions.OnExit = jsAction;           // blur
                // OnModifyCharacter fires for each keystroke (valid property)
                txtField.Actions.OnModifyCharacter = jsAction; // keystroke‑like logging
            }

            // -------------------------------------------------
            // Create sample form fields
            // -------------------------------------------------
            AddLoggingTextField("firstName", "First Name:", 100, 600, 300, 620);
            AddLoggingTextField("lastName",  "Last Name:",  100, 560, 300, 580);
            AddLoggingTextField("email",     "Email:",      100, 520, 300, 540);

            // Save the PDF (no PreSave required for tagged content)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with interaction logging saved to '{outputPath}'.");
    }
}
