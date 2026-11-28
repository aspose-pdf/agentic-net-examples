using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "draft_form_v4.pdf";
        const string outputPath = "cleaned_templist.pdf";
        const string listBoxName = "TempList";
        const string itemToDelete = "Temp";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor implements IDisposable, so we use a using block.
            using (FormEditor editor = new FormEditor())
            {
                // Load the PDF document.
                editor.BindPdf(inputPath);

                // Delete the specified list item from the ListBox.
                // The correct method name is DelListItem (not DeleteListItem).
                editor.DelListItem(listBoxName, itemToDelete);

                // Save the modified PDF.
                editor.Save(outputPath);
            }

            Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
