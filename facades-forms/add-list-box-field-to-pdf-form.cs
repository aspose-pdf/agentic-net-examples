using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // existing PDF to modify
        const string outputPdf = "output.pdf";  // PDF with the new list field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor is a SaveableFacade – it implements IDisposable, so wrap in using
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the source PDF
            formEditor.BindPdf(inputPdf);

            // Define the items for the list box
            formEditor.Items = new string[] { "Low", "Medium", "High" };

            // Add a ListBox field named "Priority" on page 1.
            // Parameters: field type, field name, default value, page number,
            // lower‑left X/Y, upper‑right X/Y (coordinates in points).
            formEditor.AddField(FieldType.ListBox, "Priority", "Medium", 1,
                               100f, 500f, 200f, 550f);

            // Save the modified document
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"List field added and saved to '{outputPdf}'.");
    }
}