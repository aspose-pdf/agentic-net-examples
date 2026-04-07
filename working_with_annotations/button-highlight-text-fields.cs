using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        // Create a new PDF document inside a using block for deterministic disposal.
        using (Document doc = new Document())
        {
            // Add a blank page (1‑based indexing).
            Page page = doc.Pages.Add();

            // Define the button rectangle (left, bottom, right, top).
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 550);

            // Create a push button field on the page.
            ButtonField button = new ButtonField(page, btnRect)
            {
                Name = "HighlightBtn",
                Contents = "Highlight Text Fields",
                Highlighting = HighlightingMode.Push
            };

            // JavaScript that highlights (sets border color to yellow) all text fields on the current page.
            string jsCode = @"
                var fields = this.getField();
                for (var i = 0; i < fields.length; i++) {
                    if (fields[i].type == 'text' && fields[i].page == this.pageNum) {
                        fields[i].strokeColor = color.yellow;
                    }
                }";

            // Create a JavascriptAction with the script.
            JavascriptAction jsAction = new JavascriptAction(jsCode);

            // Assign the JavaScript to the button's mouse‑up action.
            button.Actions.OnPressMouseBtn = jsAction;

            // Save the PDF.
            doc.Save("ButtonWithJs.pdf");
        }

        Console.WriteLine("PDF with button annotation created successfully.");
    }
}