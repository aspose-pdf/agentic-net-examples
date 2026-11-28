using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // for PropertyFlag

class Program
{
    static void Main()
    {
        const string inputPath = "draft_form.pdf";
        const string outputPath = "readonly_address.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor does not implement IDisposable, so we just instantiate it.
        FormEditor editor = new FormEditor();
        editor.BindPdf(inputPath);

        // Add a single‑line text field named "Address" on page 1.
        // Parameters: field type, name, page number (1‑based), llx, lly, urx, ury.
        editor.AddField(FieldType.Text, "Address", 1, 100f, 600f, 300f, 620f);

        // Mark the field as read‑only.
        editor.SetFieldAttribute("Address", PropertyFlag.ReadOnly);

        // Save the modified PDF.
        editor.Save(outputPath);

        Console.WriteLine($"Read‑only text field added. Saved to '{outputPath}'.");
    }
}
