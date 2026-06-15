using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Create a new PDF document with a single page
        using (Document doc = new Document())
        {
            Page page = doc.Pages.Add();

            // Add a sample text annotation to demonstrate toggling
            Aspose.Pdf.Rectangle textRect = new Aspose.Pdf.Rectangle(100, 600, 300, 650);
            TextAnnotation sampleAnno = new TextAnnotation(page, textRect)
            {
                Title = "Sample",
                Contents = "This annotation will be shown/hidden by the button.",
                Color = Aspose.Pdf.Color.Yellow,
                // Initially visible
                Flags = AnnotationFlags.Print
            };
            page.Annotations.Add(sampleAnno);

            // Define the button rectangle (position and size)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 500, 250, 540);

            // Create a button field (acts as a button annotation)
            ButtonField toggleButton = new ButtonField(page, buttonRect)
            {
                Name = "ToggleAnnotations",
                // Optional visual appearance (label)
                Value = "Toggle Annotations"
            };

            // JavaScript to toggle the hidden flag of all annotations on the current page
            string jsCode = @"
                var ann = this.getAnnots();
                for (var i = 0; i < ann.length; i++) {
                    ann[i].hidden = !ann[i].hidden;
                }
            ";

            // Assign the JavaScript to the button's mouse‑press action
            toggleButton.Actions.OnPressMouseBtn = new JavascriptAction(jsCode);

            // Add the button to the document's form (annotations collection)
            doc.Form.Add(toggleButton);

            // Save the resulting PDF
            string outputPath = "ToggleAnnotations.pdf";
            doc.Save(outputPath);
            Console.WriteLine($"PDF saved to '{outputPath}'. Click the button to show/hide annotations.");
        }
    }
}