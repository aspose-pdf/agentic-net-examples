using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output19.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor is a Facades API for working with PDF forms.
        // It implements IDisposable, so wrap it in a using block.
        using (FormEditor editor = new FormEditor())
        {
            // Load the existing PDF document.
            editor.BindPdf(inputPath);

            // Add a single‑line text field named "AddressLine" on page 4.
            // Parameters: FieldType, name, pageNumber, llx, lly, urx, ury.
            editor.AddField(FieldType.Text, "AddressLine", 4, 200, 250, 350, 300);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"Form field added and saved to '{outputPath}'.");
    }
}
