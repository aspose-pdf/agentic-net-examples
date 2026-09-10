using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // -------------------------------------------------
        // 0. Ensure a source PDF exists (self‑contained example)
        // -------------------------------------------------
        if (!File.Exists(inputPath))
        {
            using (Document placeholder = new Document())
            {
                // Add a single blank page – the demo works on this page
                placeholder.Pages.Add();
                placeholder.Save(inputPath);
            }
        }

        // -------------------------------------------------
        // 1. Load the existing PDF (the one we just created)
        // -------------------------------------------------
        using (Document doc = new Document(inputPath))
        {
            // Use the first page for the demo
            Page page = doc.Pages[1];

            // -------------------------------------------------
            // 2. Create a RichMediaAnnotation (initially visible)
            // -------------------------------------------------
            Aspose.Pdf.Rectangle richRect = new Aspose.Pdf.Rectangle(100, 500, 300, 700);
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, richRect)
            {
                Name = "richMedia",               // Identifier used in JavaScript
                Contents = "Sample Media"        // Optional tooltip text
                // Additional properties (e.g., RichMediaContent) can be set here
            };
            page.Annotations.Add(richMedia);

            // -------------------------------------------------
            // 3. Create a push button that will toggle the RichMediaAnnotation
            // -------------------------------------------------
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(350, 500, 450, 550);
            // NOTE: ButtonField constructor requires the *page* instance, not the document.
            ButtonField toggleButton = new ButtonField(page, btnRect)
            {
                PartialName = "toggleButton",
                Contents = "Toggle Media",
                Highlighting = HighlightingMode.Push,
                Color = Aspose.Pdf.Color.LightGray
            };

            // Optional: give the button a visible border
            toggleButton.Border = new Border(toggleButton) { Width = 1 };

            // -------------------------------------------------
            // 4. Attach JavaScript that flips the Hidden flag (value 2) of the RichMediaAnnotation
            // -------------------------------------------------
            // The script retrieves the annotation by name and XORs its flags with 2,
            // effectively toggling the Hidden flag on each click.
            string js = "var annot = this.getAnnot(this.page, 'richMedia'); " +
                        "annot.setFlags(annot.flags ^ 2);";
            JavascriptAction jsAction = new JavascriptAction(js);
            toggleButton.Actions.OnPressMouseBtn = jsAction;

            // Add the button to the document's form (AcroForm) – not directly to page.Annotations
            doc.Form.Add(toggleButton);

            // -------------------------------------------------
            // 5. Save the modified PDF
            // -------------------------------------------------
            doc.Save(outputPath);
        }
    }
}
