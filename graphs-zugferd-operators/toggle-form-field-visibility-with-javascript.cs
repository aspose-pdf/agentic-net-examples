using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // existing PDF (can be empty)
        const string outputPath = "output.pdf";

        // Load the PDF (or create a new one if the file does not exist)
        using (Document doc = File.Exists(inputPath) ? new Document(inputPath) : new Document())
        {
            // Ensure there is at least one page
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // Get the first page – all form fields must be associated with a page instance
            Page page = doc.Pages[1];

            // -------------------------------------------------
            // 1. Create the checkbox that will control visibility
            // -------------------------------------------------
            CheckboxField checkBox = new CheckboxField(page,
                new Aspose.Pdf.Rectangle(100, 700, 120, 720)); // llx, lly, urx, ury
            checkBox.Name = "ShowTargetCheck";
            checkBox.Checked = false; // initially unchecked
            doc.Form.Add(checkBox);

            // -------------------------------------------------
            // 2. Create the field that will be hidden/shown
            // -------------------------------------------------
            TextBoxField targetField = new TextBoxField(page,
                new Aspose.Pdf.Rectangle(150, 700, 350, 720));
            targetField.Name = "TargetField";
            targetField.Contents = "This field is hidden when the box is unchecked.";
            doc.Form.Add(targetField);

            // -------------------------------------------------
            // 3. Attach JavaScript to the checkbox to toggle visibility
            // -------------------------------------------------
            string js = @"
if (event.target.checked) {
    this.getField('TargetField').display = display.visible;
} else {
    this.getField('TargetField').display = display.hidden;
}";
            // OnReleaseMouseBtn fires when the user releases the mouse button after clicking the checkbox.
            checkBox.Actions.OnReleaseMouseBtn = new JavascriptAction(js);

            // -------------------------------------------------
            // 4. Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
