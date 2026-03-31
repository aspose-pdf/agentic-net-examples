using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string sourceField = "Logo";
        const string targetField = "HeaderLogo";
        const int pageNumber = -1; // same page as source field

        try
        {
            FormEditor formEditor = new FormEditor(inputPath, outputPath);
            formEditor.CopyInnerField(sourceField, targetField, pageNumber);
            formEditor.Save();
            Console.WriteLine($"Field '{sourceField}' copied to '{targetField}' and saved as '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}