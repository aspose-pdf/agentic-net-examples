using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "contract.pdf";
        const string outputPath = "cleaned.pdf";
        const string listBoxFieldName = "MyListBox"; // replace with actual field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // FormEditor does not implement IDisposable, so we instantiate it directly.
            FormEditor formEditor = new FormEditor();

            // Bind the PDF to the editor.
            formEditor.BindPdf(inputPath);

            // Delete the list item "Obsolete" from the specified ListBox field.
            // The correct method name is DelListItem (not DeleteListBoxItem).
            formEditor.DelListItem(listBoxFieldName, "Obsolete");

            // Save the modified PDF.
            formEditor.Save(outputPath);

            Console.WriteLine($"Cleaned PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
