using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";
        const string sourceField = "TemplateField";
        const string newField = "ClonedField";
        const int targetPage = 5;

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        // Initialize FormEditor with source and destination PDFs
        FormEditor formEditor = new FormEditor(inputPath, outputPath);
        // Copy the outer definition of the field to a new field on page 5
        formEditor.CopyInnerField(sourceField, newField, targetPage);
        // Persist changes
        formEditor.Save();

        Console.WriteLine("Field copied to new field '" + newField + "' on page " + targetPage);
    }
}
