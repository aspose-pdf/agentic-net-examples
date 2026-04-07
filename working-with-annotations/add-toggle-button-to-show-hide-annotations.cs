using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_with_toggle_button.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        using (PdfContentEditor editor = new PdfContentEditor())
        {
            editor.BindPdf(inputPath);

            // JavaScript that toggles the hidden flag of every annotation on the current page
            string jsCode = @"
                var annots = this.getAnnots();
                for (var i = 0; i < annots.length; i++) {
                    annots[i].hidden = !annots[i].hidden;
                }";

            // Define the clickable area for the button using fully‑qualified System.Drawing types
            System.Drawing.Rectangle buttonRect = new System.Drawing.Rectangle(50, 750, 150, 30);

            // Create a JavaScript link on page 1 (color also fully‑qualified)
            editor.CreateJavaScriptLink(jsCode, buttonRect, 1, System.Drawing.Color.Transparent);

            // Save the modified document
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with toggle button saved to '{outputPath}'.");
    }
}
