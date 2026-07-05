using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF and edit its form using FormEditor (Facades API)
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Define the items for the list box
            formEditor.Items = new string[] { "Low", "Medium", "High" };

            // Add a ListBox field named "Priority" with default value "Medium"
            // Parameters: field type, field name, initial value, page number, llx, lly, urx, ury
            formEditor.AddField(FieldType.ListBox, "Priority", "Medium", 1, 100, 500, 200, 550);

            // Save the modified PDF to the output path
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"List field 'Priority' added and saved to '{outputPath}'.");
    }
}