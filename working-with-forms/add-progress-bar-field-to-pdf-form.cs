using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input_form.pdf";
        const string outputPath = "output_with_progress.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF form
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Add a Progress Bar field (implemented as a TextBox) to the first page
            // -----------------------------------------------------------------
            // Define the rectangle for the progress bar (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle progressRect = new Aspose.Pdf.Rectangle(50, 750, 300, 770);

            // Create a TextBoxField to act as the progress bar
            TextBoxField progressBar = new TextBoxField(doc.Pages[1], progressRect)
            {
                PartialName = "ProgressBar",
                Value = "0%",
                // Background color of the field (Aspose.Pdf.Color)
                Color = Aspose.Pdf.Color.LightGray,
                // Text alignment inside the field
                TextHorizontalAlignment = HorizontalAlignment.Center
            };

            // Set text appearance (font, size, text color) via DefaultAppearance (System.Drawing.Color)
            progressBar.DefaultAppearance = new DefaultAppearance(
                FontRepository.FindFont("Helvetica"),
                12,
                System.Drawing.Color.Blue);

            // Set border color using the field's Characteristics (System.Drawing.Color)
            progressBar.Characteristics.Border = System.Drawing.Color.Black;

            // Add the progress bar to the form fields collection (page index is 1‑based)
            doc.Form.Add(progressBar, 1);

            // -----------------------------------------------------------------
            // 2. Define the form fields that represent sections (example checkboxes)
            // -----------------------------------------------------------------
            // Assume three checkbox fields already exist in the PDF: Section1, Section2, Section3
            string[] sectionFieldNames = { "Section1", "Section2", "Section3" };

            // -----------------------------------------------------------------
            // 3. Attach a JavaScript Calculate action to the progress bar
            // -----------------------------------------------------------------
            // The script counts checked sections and updates the progress bar value.
            string jsCalculate = @"
var sections = ['Section1','Section2','Section3'];
var filled = 0;
for (var i = 0; i < sections.length; i++) {
    var f = this.getField(sections[i]);
    if (f && f.value == 'Yes') {
        filled++;
    }
}
var percent = (filled / sections.length) * 100;
this.value = Math.round(percent) + '%';
";

            // Use the Actions property (not AdditionalActions) for form fields
            progressBar.Actions.OnCalculate = new JavascriptAction(jsCalculate);

            // -----------------------------------------------------------------
            // 4. Ensure each section field triggers a recalculation when changed
            // -----------------------------------------------------------------
            foreach (string fieldName in sectionFieldNames)
            {
                var field = doc.Form[fieldName] as CheckboxField;
                if (field != null)
                {
                    // When the checkbox is clicked, recalculate the progress bar.
                    // The correct action property for a mouse click is OnPressMouseBtn.
                    field.Actions.OnPressMouseBtn = new JavascriptAction("event.target.calculate();");
                }
            }

            // -----------------------------------------------------------------
            // 5. Save the modified PDF
            // -----------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with progress bar saved to '{outputPath}'.");
    }
}
