using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a sample PDF (self‑contained example)
        using (Document createDoc = new Document())
        {
            createDoc.Pages.Add();
            createDoc.Save("input.pdf");
        }

        // Open the PDF and add form fields with JavaScript actions
        using (Document doc = new Document("input.pdf"))
        {
            // 1‑based page indexing
            Page page = doc.Pages[1];

            // Checkbox that controls the optional section
            Rectangle checkboxRect = new Rectangle(100, 700, 120, 720);
            CheckboxField checkbox = new CheckboxField(page, checkboxRect);
            checkbox.PartialName = "ShowSection";
            checkbox.Checked = false;
            doc.Form.Add(checkbox);

            // Optional text box that will be shown/hidden
            Rectangle textRect = new Rectangle(100, 650, 300, 680);
            TextBoxField optionalText = new TextBoxField(page, textRect);
            optionalText.PartialName = "OptionalSection";
            optionalText.Value = "Optional content here";
            // Initial visibility will be handled by JavaScript on document open
            doc.Form.Add(optionalText);

            // JavaScript to toggle visibility based on the checkbox state
            string jsCode = "var f = this.getField('OptionalSection'); " +
                            "if (event.target.value == 'Yes') { f.display = display.visible; } " +
                            "else { f.display = display.hidden; }";
            JavascriptAction jsAction = new JavascriptAction(jsCode);
            // Use a valid action property for a button press
            checkbox.Actions.OnReleaseMouseBtn = jsAction;

            // Ensure the optional section is hidden when the document opens
            string initJs = "var f = this.getField('OptionalSection'); f.display = display.hidden;";
            doc.JavaScript["InitVisibility"] = initJs;

            doc.Save("output.pdf");
        }
    }
}
