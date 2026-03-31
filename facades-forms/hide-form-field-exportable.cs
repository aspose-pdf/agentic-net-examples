using System;
using Aspose.Pdf;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!System.IO.File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Initialize FormEditor with source and destination PDF files
        FormEditor formEditor = new FormEditor(inputPath, outputPath);
        // Hide the field "EmployeeID" while keeping it exportable (no NoExport flag)
        bool success = formEditor.SetFieldAppearance("EmployeeID", AnnotationFlags.Hidden);
        if (!success)
        {
            Console.Error.WriteLine("Failed to set field appearance for EmployeeID.");
        }

        // Persist changes to the output file
        formEditor.Save();
        Console.WriteLine($"Field 'EmployeeID' set to hidden and saved to '{outputPath}'.");
    }
}
