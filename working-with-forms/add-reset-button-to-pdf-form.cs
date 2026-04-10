using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations;      // JavascriptAction
using Aspose.Pdf.Forms;           // ButtonField

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_reset.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the existing PDF (lifecycle rule: use Document constructor)
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document contains a form
            Form form = doc.Form;
            if (form == null)
            {
                Console.Error.WriteLine("The PDF does not contain a form.");
                return;
            }

            // Create a reset button on the first page
            Page page = doc.Pages[1];

            // Define button rectangle (llx, lly, urx, ury)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 200, 530);

            // Instantiate the button field and set its visual properties
            ButtonField resetButton = new ButtonField(page, btnRect);
            resetButton.PartialName = "ResetButton";
            resetButton.AlternateCaption = "Reset";
            resetButton.Border = new Border(resetButton) { Width = 1 };
            resetButton.Color = Color.LightGray;

            // Assign a JavaScript action that resets the form to its default values
            // (ResetFormAction is not available via AnnotationActionCollection, so we use JavaScript)
            resetButton.Actions.OnReleaseMouseBtn = new JavascriptAction("this.resetForm();");

            // Add the button annotation to the page
            page.Annotations.Add(resetButton);

            // Save the modified PDF (lifecycle rule: use Document.Save)
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with reset button saved to '{outputPath}'.");
    }
}
