using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "EmployeeID";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF for form editing
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Set the field to be hidden (but still exportable)
            editor.SetFieldAppearance(fieldName, AnnotationFlags.Hidden);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field '{fieldName}' set to hidden (exportable) and saved to '{outputPath}'.");
    }
}