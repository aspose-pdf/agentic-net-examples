using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Annotations; // JavascriptAction, AnnotationFlags
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_button.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Get the first page (Aspose.Pdf uses 1‑based indexing)
            Page page = doc.Pages[1];

            // Define the button rectangle (lower‑left X/Y, upper‑right X/Y)
            Aspose.Pdf.Rectangle btnRect = new Aspose.Pdf.Rectangle(100, 500, 250, 550);

            // Create a push‑button form field
            ButtonField exportButton = new ButtonField(page, btnRect)
            {
                PartialName    = "ExportAnnotationsBtn",
                AlternateName  = "Export",
                NormalCaption  = "Export Annotations"
            };

            // Assign JavaScript to the button's click action (OnActivated)
            exportButton.OnActivated = new JavascriptAction(@"
                var annots = this.getAnnots();
                var data = [];
                for (var i = 0; i < annots.length; i++) {
                    var a = annots[i];
                    data.push({
                        type: a.type,
                        contents: a.contents,
                        rect: a.rect
                    });
                }
                var json = JSON.stringify(data);
                console.println(json);
            ");

            // Add the button to the document's form collection (page already supplied in constructor)
            doc.Form.Add(exportButton, 1);

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF with export button saved to '{outputPath}'.");
    }
}
