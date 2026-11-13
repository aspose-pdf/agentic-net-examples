using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "template.pdf";   // optional existing PDF, can be empty
        const string outputPath = "form_with_js.pdf";

        // Ensure the input file exists; if not, create a blank PDF with one page
        if (!File.Exists(inputPath))
        {
            using (Document blank = new Document())
            {
                blank.Pages.Add();
                blank.Save(inputPath);
            }
        }

        // Load the PDF, add form fields and JavaScript, then save
        using (Document doc = new Document(inputPath))
        {
            // Ensure there is at least one page to place the fields on
            if (doc.Pages.Count == 0)
                doc.Pages.Add();

            // ----- Create a combo box that controls visibility -----
            Aspose.Pdf.Rectangle comboRect = new Aspose.Pdf.Rectangle(100, 700, 250, 730);
            ComboBoxField combo = new ComboBoxField(doc.Pages[1], comboRect);
            combo.PartialName = "optionSelector";
            // Add options (export value first, display name second). Order matters for default selection.
            combo.AddOption("Show");
            combo.AddOption("Hide");
            // Default selection: "Hide" (second option, 1‑based index)
            combo.Selected = 2;
            doc.Form.Add(combo);

            // ----- Create optional section fields (initially hidden) -----
            Aspose.Pdf.Rectangle txtRect1 = new Aspose.Pdf.Rectangle(100, 650, 300, 680);
            TextBoxField txt1 = new TextBoxField(doc.Pages[1], txtRect1);
            txt1.PartialName = "section1_field1";
            txt1.Value = "Optional Field 1";
            doc.Form.Add(txt1);

            Aspose.Pdf.Rectangle txtRect2 = new Aspose.Pdf.Rectangle(100, 600, 300, 630);
            TextBoxField txt2 = new TextBoxField(doc.Pages[1], txtRect2);
            txt2.PartialName = "section1_field2";
            txt2.Value = "Optional Field 2";
            doc.Form.Add(txt2);

            // Hide the optional fields initially using the correct action property
            txt1.Actions.OnOpenPage = new JavascriptAction("this.getField('section1_field1').display = display.hidden;");
            txt2.Actions.OnOpenPage = new JavascriptAction("this.getField('section1_field2').display = display.hidden;");

            // ----- JavaScript to toggle visibility based on combo selection -----
            string js = @"
var sel = this.getField('optionSelector').value;
if (sel == 'Show') {
    this.getField('section1_field1').display = display.visible;
    this.getField('section1_field2').display = display.visible;
} else {
    this.getField('section1_field1').display = display.hidden;
    this.getField('section1_field2').display = display.hidden;
}";
            // Use a supported action property for value change
            combo.Actions.OnCalculate = new JavascriptAction(js);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with JavaScript saved to '{outputPath}'.");
    }
}
