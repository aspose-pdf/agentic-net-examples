using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations; // for AnnotationFlags enum

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Use FormEditor (the correct facade) to manipulate form fields
        using (var editor = new FormEditor())
        {
            editor.BindPdf(inputPath);

            // Hide the field "EmployeeID" but keep it exportable.
            // Use AnnotationFlags.Hidden via SetFieldAppearance; this does not set the NoExport flag.
            editor.SetFieldAppearance("EmployeeID", AnnotationFlags.Hidden);

            // Save the modified PDF
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field \"EmployeeID\" set to hidden (exportable). Saved to '{outputPath}'.");
    }
}
