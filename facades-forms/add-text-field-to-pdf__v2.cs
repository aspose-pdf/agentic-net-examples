using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "Commented.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block.
        using (FormEditor editor = new FormEditor())
        {
            // Load the existing PDF.
            editor.BindPdf(inputPath);

            // Choose the page where the field will be placed.
            int pageNumber = 1; // first page

            // Define the rectangle for the text field (lower‑left x, lower‑left y, upper‑right x, upper‑right y).
            // These coordinates place the field near the bottom of the page.
            double llx = 50;   // left
            double lly = 50;   // bottom
            double urx = 300;  // right
            double ury = 80;   // top

            // Insert a text field named "Comment" using the correct FormEditor API.
            editor.AddField(FieldType.Text, "Comment", pageNumber,
                (float)llx, (float)lly, (float)urx, (float)ury);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Text field added and saved to '{outputPath}'.");
    }
}
