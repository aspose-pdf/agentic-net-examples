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

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Work with the first page (pages are 1‑based)
            Page page = doc.Pages[1];

            // -----------------------------------------------------------------
            // 1. Create the RichMedia annotation that we want to show/hide.
            //    Give it a unique name so it can be referenced from JavaScript.
            // -----------------------------------------------------------------
            var richMedia = new RichMediaAnnotation(
                page,
                new Aspose.Pdf.Rectangle(100, 500, 300, 700) // left, bottom, right, top
            );
            richMedia.Name = "myRichMedia";
            // Additional RichMediaContent can be set here if needed
            page.Annotations.Add(richMedia);

            // -----------------------------------------------------------------
            // 2. JavaScript that toggles the 'hidden' flag of the RichMedia annotation.
            //    this.getAnnot(pageIndex, name) returns the annotation object.
            //    The page index is zero‑based in JavaScript, so page 1 => 0.
            // -----------------------------------------------------------------
            string jsToggle = "var a = this.getAnnot(0, 'myRichMedia'); a.hidden = !a.hidden;";

            // -----------------------------------------------------------------
            // 3. Create a button field that executes the JavaScript on click.
            // -----------------------------------------------------------------
            var toggleButton = new ButtonField(
                page,
                new Aspose.Pdf.Rectangle(350, 500, 450, 550) // left, bottom, right, top
            )
            {
                Name = "toggleButton",
                // Optional visual appearance – set the displayed caption
                Value = "Toggle RichMedia"
            };
            // Assign the JavaScript action to the mouse‑press event (valid property)
            toggleButton.Actions.OnPressMouseBtn = new JavascriptAction(jsToggle);
            page.Annotations.Add(toggleButton);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Annotated PDF saved to '{outputPath}'.");
    }
}
