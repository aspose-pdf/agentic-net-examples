using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;
using Aspose.Pdf.Forms;

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
            // Work with the first page (change index as needed)
            Page page = doc.Pages[1];

            // Define the button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create a button form field (widget annotation) on the page
            ButtonField btn = new ButtonField(page, btnRect)
            {
                PartialName = "toggleBtn"
            };

            // Required appearance settings before setting the caption
            btn.DefaultAppearance = new DefaultAppearance("Helvetica", 12, System.Drawing.Color.Black);
            btn.AlternateCaption = "Toggle Annotations"; // button label

            // Visual appearance – background colour and border colour
            btn.Characteristics.Background = System.Drawing.Color.LightGray;
            btn.Color = Aspose.Pdf.Color.Black; // border colour

            // Border must be created after the button instance because it requires the parent annotation
            btn.Border = new Border(btn) { Width = 1, Style = BorderStyle.Solid };

            // JavaScript that toggles the hidden flag of every annotation on the current page
            string js = @"
var annots = this.getAnnots();
if (annots != null) {
    for (var i = 0; i < annots.length; i++) {
        var a = annots[i];
        a.hidden = !a.hidden;
    }
}
";

            // Assign the JavaScript action to the button – use OnActivated (or Actions.OnPressMouseBtn)
            btn.OnActivated = new JavascriptAction(js);

            // Add the button to the document's AcroForm (page number 1)
            doc.Form.Add(btn, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with toggle button saved to '{outputPath}'.");
    }
}
