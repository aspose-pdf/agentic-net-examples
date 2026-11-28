using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "draft.pdf";
        const string outputPath = "flagged.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block
        using (FormEditor editor = new FormEditor())
        {
            // Load the existing PDF
            editor.BindPdf(inputPath);

            // Define the rectangle (llx, lly, urx, ury) for the new text field
            int pageNumber = 1; // pages are 1‑based
            float llx = 100f, lly = 600f, urx = 300f, ury = 650f;

            // Add a text field named "Comments"
            editor.AddField(FieldType.Text, "Comments", pageNumber, llx, lly, urx, ury);

            // Set the Print appearance flag using AnnotationFlags
            editor.SetFieldAppearance("Comments", AnnotationFlags.Print);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Text field 'Comments' added and saved to '{outputPath}'.");
    }
}
