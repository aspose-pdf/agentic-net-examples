using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "Cities.pdf";          // source PDF containing the form
        const string outputPath = "CitiesUpdated.pdf"; // result PDF

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // -----------------------------------------------------------------
            // 1. Structural edit – add a new list item to the existing list field.
            // -----------------------------------------------------------------
            using (FormEditor editor = new FormEditor())
            {
                editor.BindPdf(inputPath);
                // Add the new item "Metropolis" to the list field named "Cities".
                editor.AddListItem("Cities", "Metropolis");
                // Save the PDF after the structural change.
                editor.Save(outputPath);
            }

            // ---------------------------------------------------------------
            // 2. Data edit – set the newly added item as the default selection.
            // ---------------------------------------------------------------
            using (Form form = new Form())
            {
                form.BindPdf(outputPath);
                // Fill the field with the value that should be selected by default.
                form.FillField("Cities", "Metropolis");
                // Overwrite the same file with the filled value.
                form.Save(outputPath);
            }

            Console.WriteLine($"Form updated and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
