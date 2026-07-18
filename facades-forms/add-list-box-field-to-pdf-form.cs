using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the input file exists
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Use FormEditor to modify the PDF form
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the existing PDF
            formEditor.BindPdf(inputPdf);

            // Define the items for the list box
            formEditor.Items = new string[] { "Low", "Medium", "High" };

            // Add a ListBox field named "Priority" with default selection "Medium"
            // Parameters: field type, field name, default value, page number, llx, lly, urx, ury
            formEditor.AddField(
                FieldType.ListBox,
                "Priority",
                "Medium",
                1,
                100f,   // lower‑left X
                500f,   // lower‑left Y
                200f,   // upper‑right X
                600f    // upper‑right Y
            );

            // Save the modified PDF
            formEditor.Save(outputPdf);
        }

        Console.WriteLine($"List field \"Priority\" added and saved to '{outputPdf}'.");
    }
}