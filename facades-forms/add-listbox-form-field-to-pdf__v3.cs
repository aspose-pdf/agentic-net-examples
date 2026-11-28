using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "listbox_added.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Items that will appear in the ListBox.
        string[] items = { "Option 1", "Option 2", "Option 3" };

        // Rectangle for the field: lower‑left (llx,lly), upper‑right (urx,ury).
        float llx = 100f, lly = 500f, urx = 200f, ury = 550f;

        // FormEditor implements IDisposable – use a using block for deterministic cleanup.
        using (var editor = new FormEditor())
        {
            // Load the existing PDF.
            editor.BindPdf(inputPath);

            // Add a ListBox field on page 2.
            editor.AddField(FieldType.ListBox, "MyListBox", 2, llx, lly, urx, ury);

            // Populate the ListBox with its items.
            foreach (var item in items)
                editor.AddListItem("MyListBox", item);

            // Save the modified PDF.
            editor.Save(outputPath);
        }

        Console.WriteLine($"ListBox added and saved to '{outputPath}'.");
    }
}
