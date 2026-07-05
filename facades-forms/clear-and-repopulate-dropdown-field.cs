using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths to the source PDF and the output PDF
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";

        // New items to populate the dropdown (combo box) field
        string[] newItems = new string[] { "USA", "Canada", "Mexico" };

        // Ensure the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Source file not found: {inputPdf}");
            return;
        }

        // Use FormEditor (a SaveableFacade) inside a using block for deterministic disposal
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Remove the existing dropdown field (if it exists)
            // This effectively clears all previously added list items.
            formEditor.RemoveField("CountryList");

            // Set the items that will be added to the newly created combo box
            formEditor.Items = newItems;

            // Add a new combo box field with the same name.
            // Parameters: field type, field name, default value, page number,
            //            lower‑left X, lower‑left Y, upper‑right X, upper‑right Y.
            // Adjust the rectangle coordinates as needed for your document.
            formEditor.AddField(
                FieldType.ComboBox,          // field type
                "CountryList",               // field name
                newItems[0],                 // default selected value
                1,                           // page number (1‑based)
                100f, 500f, 200f, 550f);     // rectangle coordinates

            // Save the modified PDF
            formEditor.Save();
        }

        Console.WriteLine($"Dropdown field updated and saved to '{outputPdf}'.");
    }
}