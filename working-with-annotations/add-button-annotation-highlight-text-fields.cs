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
            // Choose the page where the button will be placed (first page in this example)
            Page page = doc.Pages[1];

            // Define the button rectangle (coordinates are in points; lower‑left to upper‑right)
            Aspose.Pdf.Rectangle buttonRect = new Aspose.Pdf.Rectangle(100, 700, 200, 730);

            // Create a push button field on the selected page
            ButtonField button = new ButtonField(page, buttonRect)
            {
                // Optional visual properties
                Color   = Aspose.Pdf.Color.LightGray,
                NormalCaption = "Highlight Text Fields"
            };

            // JavaScript that highlights all text fields on the current page
            string jsCode = @"
                var pageNum = this.pageNum;
                for (var i = 0; i < this.numFields; i++) {
                    var f = this.getField(this.getFieldName(i));
                    if (f.type == 'text' && f.page == pageNum) {
                        f.fillColor = color.yellow;
                    }
                }
            ";

            // Assign the JavaScript to the button's mouse‑up action
            button.Actions.OnReleaseMouseBtn = new JavascriptAction(jsCode);

            // Add the button to the page's annotation collection
            page.Annotations.Add(button);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with button annotation: '{outputPath}'.");
    }
}