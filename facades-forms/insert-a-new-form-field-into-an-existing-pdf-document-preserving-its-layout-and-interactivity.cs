using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;   // FormEditor and FieldType are defined here

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // existing PDF with form (if any)
        const string outputPdf = "output_with_new_field.pdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Wrap the FormEditor in a using block to ensure proper disposal.
        // The constructor takes the source PDF path and the destination PDF path.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Add a new text field named "NewTextField" on page 1.
            // Coordinates are in points (1/72 inch). Adjust as needed.
            // llx, lly = lower‑left corner; urx, ury = upper‑right corner.
            bool added = formEditor.AddField(
                FieldType.Text,          // type of the field
                "NewTextField",          // field name (must be unique)
                1,                       // page number (1‑based indexing)
                100f, 500f, 250f, 530f   // rectangle: llx, lly, urx, ury
            );

            if (!added)
            {
                Console.Error.WriteLine("Failed to add the new field.");
                return;
            }

            // Save the modified PDF. The Save method writes to the destination path
            // specified in the constructor, so no additional parameters are needed.
            formEditor.Save();
        }

        Console.WriteLine($"New form field added and saved to '{outputPdf}'.");
    }
}