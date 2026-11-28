using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "questionnaire.pdf";
        const string outputPath = "choices.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor implements IDisposable, so wrap it in a using block.
            using (FormEditor editor = new FormEditor())
            {
                // Bind the existing PDF document.
                editor.BindPdf(inputPath);

                // Define the rectangle where the ListBox will appear (llx, lly, urx, ury).
                float llx = 100f;
                float lly = 500f;
                float urx = 200f;
                float ury = 550f;

                // Add a ListBox field named "Choices" on page 1.
                editor.AddField(FieldType.ListBox, "Choices", 1, llx, lly, urx, ury);

                // Populate the ListBox with items.
                string[] items = { "Option 1", "Option 2", "Option 3" };
                foreach (var item in items)
                {
                    editor.AddListItem("Choices", item);
                }

                // Save the modified PDF.
                editor.Save(outputPath);
            }

            Console.WriteLine($"ListBox field added and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
