using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "template.pdf";   // existing PDF to edit
        const string outputPdf = "output.pdf";     // result PDF

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor implements IDisposable, so wrap in using
        using (FormEditor editor = new FormEditor(inputPdf, outputPdf))
        {
            // Define the list items
            editor.Items = new string[] { "Low", "Medium", "High" };

            // Add a ListBox field named "Priority" on page 1.
            // Set the default selected value to "Medium".
            // Rectangle coordinates: lower‑left (100,500), upper‑right (200,550)
            editor.AddField(FieldType.ListBox, "Priority", "Medium", 1, 100, 500, 200, 550);

            // Persist changes to the output file
            editor.Save();
        }

        Console.WriteLine($"List field 'Priority' added and saved to '{outputPdf}'.");
    }
}