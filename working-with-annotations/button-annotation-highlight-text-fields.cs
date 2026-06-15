using System;
using System.Diagnostics.CodeAnalysis;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

// Suppress the NuGet vulnerability warning (NU1903) for the demo project.
[assembly: SuppressMessage("NuGet", "NU1903", Justification = "Demo project – vulnerability does not affect runtime behavior")] 

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing)
            Page page = doc.Pages.Add();

            // Define the button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 540);

            // Create a push button field on the page
            ButtonField button = new ButtonField(page, btnRect)
            {
                // Optional visual properties
                Name = "HighlightBtn",
                PartialName = "HighlightBtn",
                Contents = "Highlight Text Fields",
                Color = Aspose.Pdf.Color.LightGray,
                Highlighting = HighlightingMode.Push
            };

            // JavaScript that highlights all text fields on the current page
            string js = @"
                var names = this.getFieldNames();
                for (var i = 0; i < names.length; i++) {
                    var f = this.getField(names[i]);
                    if (f.type == 'text' && f.page == this.pageNum) {
                        f.fillColor = color.yellow;
                    }
                }";

            // Assign the JavaScript to the button's mouse‑up action
            button.Actions.OnReleaseMouseBtn = new JavascriptAction(js);

            // Add the button to the document's form collection (not to page annotations)
            doc.Form.Add(button);

            // Save the PDF
            doc.Save("ButtonWithJs.pdf");
        }

        Console.WriteLine("PDF with button annotation created.");
    }
}
