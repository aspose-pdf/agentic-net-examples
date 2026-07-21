using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "template.pdf";   // existing PDF or blank template
        const string outputPath = "form_with_progress.pdf";

        // Ensure the input file exists; if not, create a blank document
        if (!File.Exists(inputPath))
        {
            using (Document blank = new Document())
            {
                // Add a single empty page
                blank.Pages.Add();
                blank.Save(inputPath);
            }
        }

        // Open the document inside a using block (lifecycle rule)
        using (Document doc = new Document(inputPath))
        {
            // -----------------------------------------------------------------
            // 1. Create a progress bar field (ButtonField) that will display %.
            // -----------------------------------------------------------------
            // Rectangle: left, bottom, right, top (coordinates in points)
            Aspose.Pdf.Rectangle progressRect = new Aspose.Pdf.Rectangle(50, 750, 550, 770);
            // NOTE: Pass the target page (first page) to the field constructor.
            ButtonField progressBar = new ButtonField(doc.Pages[1], progressRect)
            {
                PartialName = "ProgressBar",
                // Light gray background; the actual value will be shown as text.
                Color = Aspose.Pdf.Color.LightGray,
                // Initial value
                Value = "0%"
            };
            // Add the progress bar to the form
            doc.Form.Add(progressBar);

            // ---------------------------------------------------------------
            // 2. Create two example section fields (TextBoxField) for the user.
            // ---------------------------------------------------------------
            // Section 1
            Aspose.Pdf.Rectangle sec1Rect = new Aspose.Pdf.Rectangle(50, 700, 250, 720);
            TextBoxField section1 = new TextBoxField(doc.Pages[1], sec1Rect)
            {
                PartialName = "Section1",
                // Optional visual styling – white background
                Color = Aspose.Pdf.Color.White
            };
            // Set border after the field is instantiated (Border needs the parent field)
            section1.Border = new Border(section1) { Width = 1 };
            doc.Form.Add(section1);

            // Section 2
            Aspose.Pdf.Rectangle sec2Rect = new Aspose.Pdf.Rectangle(50, 660, 250, 680);
            TextBoxField section2 = new TextBoxField(doc.Pages[1], sec2Rect)
            {
                PartialName = "Section2",
                Color = Aspose.Pdf.Color.White
            };
            section2.Border = new Border(section2) { Width = 1 };
            doc.Form.Add(section2);

            // ---------------------------------------------------------------
            // 3. Attach JavaScript to each section field to update the progress.
            // ---------------------------------------------------------------
            // JavaScript that calculates filled sections and updates the progress bar.
            string jsUpdateProgress = @"
var total = 2;
var filled = 0;
if (this.getField('Section1').value != '') filled++;
if (this.getField('Section2').value != '') filled++;
var percent = Math.round((filled / total) * 100);
this.getField('ProgressBar').value = percent + '%';
";

            // Use a valid action property – OnCalculate is triggered when the field value is recalculated.
            section1.Actions.OnCalculate = new JavascriptAction(jsUpdateProgress);
            section2.Actions.OnCalculate = new JavascriptAction(jsUpdateProgress);

            // ---------------------------------------------------------------
            // 4. Save the modified PDF.
            // ---------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with progress bar saved to '{outputPath}'.");
    }
}
