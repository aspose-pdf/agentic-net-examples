using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "contract_form_v3.pdf";
        const string outputPath = "signatories_box.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable; use using for deterministic disposal
        using (FormEditor editor = new FormEditor())
        {
            // Load the source PDF
            editor.BindPdf(inputPath);

            // Define the rectangle for the ListBox (lower‑left x, lower‑left y, upper‑right x, upper‑right y)
            float llx = 100f;
            float lly = 500f;
            float urx = 300f;
            float ury = 550f;

            // Add an empty ListBox field named "Signatories" on page 4
            // Use AddField with FieldType.ListBox – there is no AddListBox method.
            editor.AddField(FieldType.ListBox, "Signatories", 4, llx, lly, urx, ury);

            // If you need to pre‑populate items, call editor.AddListItem("Signatories", "Item");
            // In this task we keep the list box empty, so no AddListItem calls.

            // Save the modified document
            editor.Save(outputPath);
        }

        Console.WriteLine($"ListBox added and saved to '{outputPath}'.");
    }
}
