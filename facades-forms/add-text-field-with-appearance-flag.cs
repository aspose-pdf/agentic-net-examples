using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // AnnotationFlags enum

class Program
{
    static void Main()
    {
        const string inputPath = "draft_form_v5.pdf";
        const string outputPath = "notes_flag.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Rectangle coordinates for the field (llx, lly, urx, ury)
        float llx = 100f;
        float lly = 500f;
        float urx = 300f;
        float ury = 530f;

        // FormEditor implements IDisposable – use a using block
        using (FormEditor editor = new FormEditor())
        {
            // Load the existing PDF
            editor.BindPdf(inputPath);

            // Add a single‑line text field named "Notes" on page 1
            editor.AddField(FieldType.Text, "Notes", 1, llx, lly, urx, ury);

            // Set an appearance‑related flag (e.g., Print) for the field
            editor.SetFieldAppearance("Notes", AnnotationFlags.Print);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"PDF with appearance flag saved to '{outputPath}'.");
    }
}
