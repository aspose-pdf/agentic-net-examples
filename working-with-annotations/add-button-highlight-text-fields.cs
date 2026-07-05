using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output_with_button.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Define the button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 700, 200, 750);

            // Create a push button field on the first page
            ButtonField highlightBtn = new ButtonField(doc, btnRect);
            highlightBtn.Name = "HighlightAllTextFields";
            highlightBtn.PartialName = "HighlightAllTextFields";
            highlightBtn.Contents = "Highlight Text Fields";

            // JavaScript to highlight all text fields on the current page
            string jsCode = @"
                var curPage = this.pageNum; // current page (1‑based)
                for (var i = 0; i < this.numFields; i++) {
                    var f = this.getField(this.getFieldName(i));
                    if (f.type == 'text' && f.page == curPage) {
                        f.fillColor = color.yellow;
                    }
                }";
            JavascriptAction jsAction = new JavascriptAction(jsCode);

            // Assign the JavaScript to the button's mouse‑up action
            highlightBtn.Actions.OnPressMouseBtn = jsAction;

            // Add the button to the page's annotation collection
            doc.Pages[1].Annotations.Add(highlightBtn);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with button annotation: '{outputPath}'.");
    }
}