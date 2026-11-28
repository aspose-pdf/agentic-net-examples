using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output28.pdf";
        const string fieldName = "DepartmentList";
        const string newItem = "HR";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to FormEditor, add the list item, and save
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);
            formEditor.AddListItem(fieldName, newItem);
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"List item added and saved to '{outputPath}'.");
    }
}