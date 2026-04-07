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
        // Create a new PDF document
        using (Document doc = new Document())
        {
            // Add a single page
            Page page = doc.Pages.Add();

            // -------------------------------------------------
            // 1. Create a progress bar field (implemented as a button)
            // -------------------------------------------------
            // Rectangle: left, bottom, right, top
            Aspose.Pdf.Rectangle progressBarRect = new Aspose.Pdf.Rectangle(50, 750, 550, 770);
            // The field must be associated with a page, not only the document
            ButtonField progressBar = new ButtonField(page, progressBarRect);
            progressBar.PartialName = "ProgressBar";
            progressBar.Color = Aspose.Pdf.Color.LightGray;
            progressBar.Border = new Border(progressBar) { Width = 1 };
            progressBar.Value = "0%"; // Show initial progress text
            // Add the progress bar to the form
            doc.Form.Add(progressBar);

            // -------------------------------------------------
            // 2. Create sample sections with check boxes
            // -------------------------------------------------
            const int totalSections = 3;
            for (int i = 1; i <= totalSections; i++)
            {
                // Position each checkbox vertically
                double y = 700 - (i - 1) * 30;
                Aspose.Pdf.Rectangle cbRect = new Aspose.Pdf.Rectangle(50, y, 70, y + 20);
                // Associate the checkbox with the page
                CheckboxField cb = new CheckboxField(page, cbRect)
                {
                    PartialName = $"Section{i}",
                    // Unchecked by default
                    Checked = false,
                    // When the user clicks the checkbox, run JavaScript to update progress
                    OnActivated = new JavascriptAction("UpdateProgress();")
                };
                doc.Form.Add(cb);

                // Add a label next to the checkbox
                TextFragment tf = new TextFragment($"Section {i}");
                tf.Position = new Position(80, y + 2);
                tf.TextState.FontSize = 12;
                page.Paragraphs.Add(tf);
            }

            // -------------------------------------------------
            // 3. Add a JavaScript function that recalculates the progress bar
            // -------------------------------------------------
            // The script counts how many check boxes are checked and updates the button's value.
            string js = @"
function UpdateProgress()
{
    var total = " + totalSections + @";
    var checked = 0;
    for (var i = 1; i <= total; i++)
    {
        var field = this.getField('Section' + i);
        if (field && field.value == 'Yes')
            checked++;
    }
    var percent = Math.round((checked / total) * 100);
    var progField = this.getField('ProgressBar');
    if (progField)
        progField.value = percent + '%';
}
";
            // Attach the script to the document – use OpenAction for document‑open JavaScript
            doc.OpenAction = new JavascriptAction(js);

            // -------------------------------------------------
            // 4. Save the PDF
            // -------------------------------------------------
            const string outputPath = "ProgressBarForm.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF with progress bar saved to '{outputPath}'.");
        }
    }
}
