using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output20.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Sample items for the list box; replace with actual department names as needed
        string[] departmentItems = new[] { "HR", "Finance", "IT", "Marketing" };

        // FormEditor implements IDisposable, so wrap it in a using block
        using (FormEditor editor = new FormEditor())
        {
            // Load the existing PDF
            editor.BindPdf(inputPath);

            // Populate the Items collection before adding a ListBox, ComboBox or Radio field
            editor.Items = departmentItems;

            // Add a ListBox field named "DepartmentList" on page 1
            // Rectangle coordinates: lower‑left (50,400), upper‑right (150,450)
            editor.AddField(FieldType.ListBox, "DepartmentList", 1, 50, 400, 150, 450);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"ListBox added and saved to '{outputPath}'.");
    }
}
