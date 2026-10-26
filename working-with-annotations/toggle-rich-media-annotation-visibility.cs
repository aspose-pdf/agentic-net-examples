using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Text;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            Page page = doc.Pages[1];

            // ------------------------------------------------------------
            // 1. Create a RichMediaAnnotation (placeholder content)
            // ------------------------------------------------------------
            Rectangle richMediaRect = new Rectangle(100, 500, 400, 800);
            RichMediaAnnotation richMedia = new RichMediaAnnotation(page, richMediaRect)
            {
                Name = "MyRichMedia",
                // Use the annotation's Color property to make it visible initially
                Color = Color.LightGray
            };
            // Optional: give the annotation a thin border
            richMedia.Border = new Border(richMedia) { Width = 1 };
            page.Annotations.Add(richMedia);

            // ------------------------------------------------------------
            // 2. Create a button annotation that toggles the rich media visibility
            // ------------------------------------------------------------
            Rectangle buttonRect = new Rectangle(50, 450, 150, 480);
            ButtonField toggleButton = new ButtonField(page, buttonRect)
            {
                Name = "ToggleRichMediaButton",
                AlternateCaption = "Toggle Media",
                // Border color (background colour for a button field is not exposed directly)
                Color = Color.Aqua
            };
            // Set a thin border for the button
            toggleButton.Border = new Border(toggleButton) { Width = 1 };

            // JavaScript to toggle the 'hidden' property of the rich media annotation
            string js = @"\
                var annot = this.getAnnot(this.pageNum, 'MyRichMedia');\
                if (annot != null) {\
                    annot.hidden = !annot.hidden;\
                }\
            ";

            // Assign the JavaScript action to the button's mouse‑press event (valid action property)
            toggleButton.Actions.OnPressMouseBtn = new JavascriptAction(js);

            // Add the button to the document's form collection
            doc.Form.Add(toggleButton);

            // ------------------------------------------------------------
            // Save the modified PDF
            // ------------------------------------------------------------
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with toggle button: '{outputPath}'");
    }
}
