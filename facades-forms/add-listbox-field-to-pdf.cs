using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "contract_draft.pdf";
        const string outputPath = "terms_box.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Items to display in the list box
        string[] items = new string[]
        {
            "Term 1",
            "Term 2",
            "Term 3"
        };

        // Create a FormEditor, bind the PDF, add the ListBox, and save
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Rectangle coordinates for the ListBox on page 3 (float literals)
            float llx = 100f; // lower‑left X
            float lly = 500f; // lower‑left Y
            float urx = 200f; // upper‑right X
            float ury = 600f; // upper‑right Y

            // Set the items that will populate the ListBox (optional – can also use AddOption after creation)
            editor.Items = items;

            // Add a ListBox field (FieldType.ListBox) on page 3
            editor.AddField(FieldType.ListBox, "Terms", 3, llx, lly, urx, ury);

            // If you prefer adding items after the field is created, uncomment the following:
            // foreach (var item in items)
            //     editor.AddListItem("Terms", item);

            editor.Save(outputPath);
        }

        Console.WriteLine($"ListBox field added and saved to '{outputPath}'.");
    }
}
