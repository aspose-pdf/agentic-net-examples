using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output23.pdf";
        const string oldName    = "DepartmentList";
        const string newName    = "DivisionList";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF with FormEditor (does not implement IDisposable).
        var editor = new FormEditor();
        editor.BindPdf(inputPath);

        // Use the Form facade to read existing field names.
        var form = new Form();
        form.BindPdf(inputPath);
        if (form.FieldNames != null && form.FieldNames.Contains(oldName))
        {
            editor.RenameField(oldName, newName);
        }
        else
        {
            Console.Error.WriteLine($"Field '{oldName}' not found in the document.");
        }

        // Save the modified PDF.
        editor.Save(outputPath);
        Console.WriteLine($"Renamed field saved to '{outputPath}'.");
    }
}
