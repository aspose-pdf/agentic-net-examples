using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "output085.pdf";
        const string fieldName = "CountryList";
        const string newItem = "Germany";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor does NOT implement IDisposable, so we instantiate it directly.
            FormEditor editor = new FormEditor();

            // Bind the existing PDF document.
            editor.BindPdf(inputPath);

            // Add a new item to the existing list‑box field.
            // The FormEditor API provides AddListItem(fieldName, itemValue).
            editor.AddListItem(fieldName, newItem);

            // Save the modified PDF.
            editor.Save(outputPath);

            // Close the editor (optional but good practice).
            editor.Close();

            Console.WriteLine($"Item '{newItem}' added to list box '{fieldName}'. Saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
