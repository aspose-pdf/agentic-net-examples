using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Annotations;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "EmployeeID";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF, modify the field, and save the result
        using (FormEditor editor = new FormEditor())
        {
            // Initialize the facade with the source PDF
            editor.BindPdf(inputPdf);

            // Set the field to be hidden (AnnotationFlags.Hidden) while keeping it exportable.
            // NoExport flag is not applied, so the field will be submitted with form data.
            editor.SetFieldAppearance(fieldName, AnnotationFlags.Hidden);

            // Persist the changes to a new file
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Field '{fieldName}' set to hidden and saved to '{outputPdf}'.");
    }
}