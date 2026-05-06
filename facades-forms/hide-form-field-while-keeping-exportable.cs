using System;
using System.IO;
using Aspose.Pdf.Facades;          // FormEditor, Form
using Aspose.Pdf.Annotations;      // AnnotationFlags

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing the form
        const string outputPdf = "output.pdf";  // PDF after modifying the field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor implements IDisposable, so wrap it in a using block.
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF document.
            editor.BindPdf(inputPdf);

            // Set the "EmployeeID" field to be hidden.
            // AnnotationFlags.Hidden makes the field invisible in the viewer.
            // Do NOT set NoExport, so the field will still be included in form submissions.
            bool success = editor.SetFieldAppearance("EmployeeID", AnnotationFlags.Hidden);
            if (!success)
            {
                Console.Error.WriteLine("Failed to set appearance for field 'EmployeeID'.");
            }

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Field 'EmployeeID' set to hidden (exportable) and saved to '{outputPdf}'.");
    }
}