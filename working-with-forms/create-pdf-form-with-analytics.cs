using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "form_with_log.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a page to the document
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // Visible text box field where the user can type
            // -------------------------------------------------
            TextBoxField userField = new TextBoxField(page,
                new Rectangle(100, 700, 300, 730))
            {
                PartialName = "UserInput",
                Value = ""
            };

            // JavaScript that appends a log entry to the hidden log field
            // whenever the user releases the mouse button after interacting.
            JavascriptAction js = new JavascriptAction(
                "var log = this.getField('Log'); " +
                "log.value += 'UserInput changed at ' + new Date().toISOString() + '\n';");

            // Use a valid action property – OnReleaseMouseBtn corresponds to mouse‑up.
            userField.Actions.OnReleaseMouseBtn = js;

            // Add the visible field to the form (page index is 1‑based)
            doc.Form.Add(userField, 1);

            // -------------------------------------------------
            // Hidden log field that stores interaction records
            // -------------------------------------------------
            TextBoxField logField = new TextBoxField(page,
                new Rectangle(0, 0, 0, 0)) // zero‑size rectangle makes it invisible
            {
                PartialName = "Log",
                Value = ""
            };

            // Mark the field as hidden so it does not appear in the UI
            logField.Flags = AnnotationFlags.Hidden;

            // Add the hidden log field to the form
            doc.Form.Add(logField, 1);

            // Save the PDF document
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with analytics log saved to '{outputPath}'.");
    }
}
