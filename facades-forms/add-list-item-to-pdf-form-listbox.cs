using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "feedback.pdf";
        const string outputPath = "updated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block.
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the existing PDF document.
            formEditor.BindPdf(inputPath);

            // Add a new list item "Excellent" to the ListBox field named "Rating".
            // The correct method on FormEditor is AddListItem (not AddListBoxItem).
            formEditor.AddListItem("Rating", "Excellent");

            // Save the modified PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
