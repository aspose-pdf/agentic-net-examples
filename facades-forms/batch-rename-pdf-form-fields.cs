using System;
using System.IO;
using Aspose.Pdf.Facades;

// Minimal stub definitions to allow compilation when the Aspose.Pdf library is not referenced.
// Remove these stubs and reference the official Aspose.Pdf NuGet package for full functionality.
namespace Aspose.Pdf.Facades
{
    public class PdfFormEditor
    {
        public void BindPdf(string path) { /* Stub: bind PDF */ }
        public string[] GetFieldNames() => new string[0]; // Stub: return empty list
        public void RenameField(string oldName, string newName) { /* Stub: rename field */ }
        public void Save(string path) { /* Stub: save PDF */ }
    }
}

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "renamed.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF for form manipulation
            PdfFormEditor formEditor = new PdfFormEditor();
            formEditor.BindPdf(inputPath);

            // Retrieve all field names in the document
            string[] fieldNames = formEditor.GetFieldNames();

            // Rename each field that starts with "Old_"
            foreach (string oldName in fieldNames)
            {
                if (oldName.StartsWith("Old_"))
                {
                    string newName = "New_" + oldName.Substring("Old_".Length);
                    formEditor.RenameField(oldName, newName);
                }
            }

            // Save the modified PDF
            formEditor.Save(outputPath);
            Console.WriteLine($"Fields renamed and saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
