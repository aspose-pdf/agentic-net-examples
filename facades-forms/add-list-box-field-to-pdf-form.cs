using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the source PDF exists – create a minimal placeholder if missing.
        if (!System.IO.File.Exists(inputPdf))
        {
            using (var placeholder = new Document())
            {
                placeholder.Pages.Add();
                placeholder.Save(inputPdf);
            }
        }

        // Create a FormEditor, bind the source PDF and add a list box field.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the existing PDF.
            formEditor.BindPdf(inputPdf);

            // Define the items for the list box.
            formEditor.Items = new string[] { "Low", "Medium", "High" };

            // Add a ListBox field named "Priority" on page 1.
            // Parameters: field type, field name, default value, page number,
            // lower‑left X, lower‑left Y, upper‑right X, upper‑right Y.
            formEditor.AddField(FieldType.ListBox, "Priority", "Medium", 1, 100, 500, 200, 550);

            // Save the modified PDF.
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"List field \"Priority\" added and saved to '{outputPdf}'.");
    }
}
