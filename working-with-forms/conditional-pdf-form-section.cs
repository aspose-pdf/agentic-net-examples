using System;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string outputPath = "conditional_form.pdf";

        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a single page (1‑based indexing)
            Page page = doc.Pages.Add();

            // -----------------------------------------------------------------
            // 1. Create the checkbox that will control the conditional section
            // -----------------------------------------------------------------
            // Position: lower‑left (x1, y1) = (50, 750), upper‑right (x2, y2) = (70, 770)
            Aspose.Pdf.Rectangle checkboxRect = new Aspose.Pdf.Rectangle(50, 750, 70, 770);
            CheckboxField showSection = new CheckboxField(page, checkboxRect);
            showSection.Name = "showSection";          // internal field name
            showSection.PartialName = "showSection";   // also used for JavaScript lookup
            showSection.ExportValue = "Yes";           // value when checked
            showSection.Color = Aspose.Pdf.Color.Blue; // visual tweak
            // Border must be assigned after the annotation instance exists
            showSection.Border = new Border(showSection) { Width = 1 };

            // -----------------------------------------------------------------
            // 2. Create the conditional field (a simple text box) – hidden initially
            // -----------------------------------------------------------------
            Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 750, 300, 770);
            TextBoxField conditionalText = new TextBoxField(page, textRect);
            conditionalText.Name = "condField";
            conditionalText.PartialName = "condField";
            // Hide the field initially
            conditionalText.Flags = AnnotationFlags.Hidden;
            // Appearance settings
            conditionalText.Color = Aspose.Pdf.Color.LightGray;
            conditionalText.Border = new Border(conditionalText) { Width = 1 };

            // -----------------------------------------------------------------
            // 3. Attach JavaScript to the checkbox to toggle visibility
            // -----------------------------------------------------------------
            // The script checks the checkbox state and sets the display property
            // of the conditional field accordingly.
            string js = @"
if (event.target.checked) {
    this.getField('condField').display = display.visible;
} else {
    this.getField('condField').display = display.hidden;
}";
            showSection.OnActivated = new JavascriptAction(js);

            // -----------------------------------------------------------------
            // 4. Add fields to the document's form
            // -----------------------------------------------------------------
            doc.Form.Add(showSection);
            doc.Form.Add(conditionalText);

            // -----------------------------------------------------------------
            // 5. Save the PDF
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with conditional section saved to '{outputPath}'.");
    }
}
