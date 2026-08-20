using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Drawing;

class Program
{
    static void Main()
    {
        const string outputPath = "FormWithAnalytics.pdf";

        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a blank page (page index is 1‑based)
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Create a hidden log field (will store interaction logs)
            // -------------------------------------------------
            // Use a zero‑size rectangle; set it read‑only and hidden
            TextBoxField logField = new TextBoxField(page, new Aspose.Pdf.Rectangle(0, 0, 0, 0));
            logField.PartialName = "Log";
            logField.ReadOnly = true;
            // Hide the field using the annotation flag (Hidden = 2)
            logField.Flags = AnnotationFlags.Hidden;
            // Add the log field to the form on page 1
            doc.Form.Add(logField, "Log", 1);

            // -------------------------------------------------
            // 2. Helper to attach JavaScript logging to a field
            // -------------------------------------------------
            void AttachLogAction(Field field, string fieldName)
            {
                // JavaScript appends a line to the hidden log field each time the field value changes
                string js = $"var log = this.getField('Log'); " +
                            $"log.value += '{fieldName} changed at ' + new Date().toISOString() + '\n';";
                // Use a valid action property – OnCalculate fires when the field value is recalculated/changed
                field.Actions.OnCalculate = new JavascriptAction(js);
            }

            // -------------------------------------------------
            // 3. Add a sample text box field
            // -------------------------------------------------
            TextBoxField txtField = new TextBoxField(page, new Aspose.Pdf.Rectangle(100, 700, 300, 730));
            txtField.PartialName = "UserName";
            txtField.Contents = "Enter name";
            doc.Form.Add(txtField, "UserName", 1);
            AttachLogAction(txtField, "UserName");

            // -------------------------------------------------
            // 4. Add a sample check box field
            // -------------------------------------------------
            CheckboxField chkField = new CheckboxField(page, new Aspose.Pdf.Rectangle(100, 650, 120, 670));
            chkField.PartialName = "Subscribe";
            chkField.Contents = "Subscribe to newsletter";
            doc.Form.Add(chkField, "Subscribe", 1);
            AttachLogAction(chkField, "Subscribe");

            // -------------------------------------------------
            // 5. Add a sample combo box field
            // -------------------------------------------------
            ComboBoxField comboField = new ComboBoxField(page, new Aspose.Pdf.Rectangle(100, 600, 300, 630));
            comboField.PartialName = "Country";
            comboField.Contents = "Select country";
            // Add options using the AddOption method (Option class has no single‑argument constructor)
            comboField.AddOption("USA");
            comboField.AddOption("Canada");
            comboField.AddOption("UK");
            doc.Form.Add(comboField, "Country", 1);
            AttachLogAction(comboField, "Country");

            // -------------------------------------------------
            // 6. Save the PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF form with analytics saved to '{outputPath}'.");
    }
}
