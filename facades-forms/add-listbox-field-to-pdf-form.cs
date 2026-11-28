using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "schedule_form_v1.pdf";
        const string outputPath = "shift_times.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Create FormEditor and bind the source PDF
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Define the rectangle for the ListBox field (llx, lly, urx, ury)
            // Note: coordinates are in points (1/72 inch) and page numbers are 1‑based.
            float llx = 100f, lly = 500f, urx = 200f, ury = 550f;

            // Add a ListBox field named "ShiftTimes" on page 2
            formEditor.AddField(FieldType.ListBox, "ShiftTimes", 2, llx, lly, urx, ury);

            // Populate the ListBox with items
            string[] shiftItems = { "Morning", "Afternoon", "Night" };
            foreach (var item in shiftItems)
            {
                formEditor.AddListItem("ShiftTimes", item);
            }

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"ListBox field added and saved to '{outputPath}'.");
    }
}
