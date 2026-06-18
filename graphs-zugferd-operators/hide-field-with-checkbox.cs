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

        // Open the sample PDF and add form fields with JavaScript logic
        using (Document doc = new Document("input.pdf"))
        {
            // Add a checkbox field
            Aspose.Pdf.Rectangle checkboxRect = new Aspose.Pdf.Rectangle(100, 700, 120, 720);
            CheckboxField checkbox = new CheckboxField(doc.Pages[1], checkboxRect);
            checkbox.Name = "myCheckbox";
            checkbox.ExportValue = "Yes";
            doc.Form.Add(checkbox);

            // Add a text box field that will be hidden/shown
            Aspose.Pdf.Rectangle textBoxRect = new Aspose.Pdf.Rectangle(100, 650, 300, 680);
            TextBoxField targetField = new TextBoxField(doc.Pages[1], textBoxRect);
            targetField.Name = "targetField";
            targetField.Contents = "This field is hidden when the checkbox is unchecked.";
            doc.Form.Add(targetField);

            // JavaScript to hide/show the target field based on checkbox state
            string js = "if (event.target.value == \"Off\") { this.getField(\"targetField\").display = display.hidden; } else { this.getField(\"targetField\").display = display.visible; }";
            JavascriptAction jsAction = new JavascriptAction(js);
            // Use a valid action property for a checkbox – OnPressMouseBtn fires when the user clicks the box
            checkbox.Actions.OnPressMouseBtn = jsAction;

            doc.Save("output.pdf");
        }
    }
}
