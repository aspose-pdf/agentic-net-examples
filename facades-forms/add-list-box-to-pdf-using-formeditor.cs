using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "output079.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPath);

            // Items to display in the list box – must be set before adding a ListBox field
            editor.Items = new[] { "USA", "Canada", "Mexico" };

            // Add a ListBox field named "CountryList" on page 1
            // Rectangle coordinates: llx, lly, urx, ury
            editor.AddField(FieldType.ListBox, "CountryList", 1, 100, 100, 250, 150);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"List box added and saved to '{outputPath}'.");
    }
}
