using System;
using System.IO;
using System.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "output118.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable – use a using block.
        using (FormEditor editor = new FormEditor())
        {
            // Bind the source PDF.
            editor.BindPdf(inputPath);

            // -----------------------------------------------------------------
            // 1. Decorate the existing field "Comments" with a gray border.
            //    FormEditor does not expose the field directly; instead we set the
            //    Facade (appearance) and call DecorateField for the target name.
            // -----------------------------------------------------------------
            editor.Facade = new FormFieldFacade
            {
                BorderColor = System.Drawing.Color.Gray, // fully qualified to avoid CS0104
                BorderWidth = 1 // optional thickness
            };
            editor.DecorateField("Comments");

            // -----------------------------------------------------------------
            // 2. Re‑position the field on page 2 at (250,250).
            //    MoveField works with the lower‑left and upper‑right corners of the
            //    field rectangle.  Here we keep the original size (100 × 50) as an
            //    example: llx=250, lly=250, urx=350, ury=300.
            // -----------------------------------------------------------------
            editor.MoveField("Comments", 250f, 250f, 350f, 300f);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
