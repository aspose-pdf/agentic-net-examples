using System;
using System.IO;
using Aspose.Pdf.Facades;          // FormEditor, Form
using Aspose.Pdf.Annotations;      // AnnotationFlags

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";      // source PDF with the form
        const string outputPdf = "output.pdf";     // PDF after modifying the field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Open the PDF for form editing using FormEditor (facade API)
        using (FormEditor editor = new FormEditor())
        {
            // Bind the existing PDF document
            editor.BindPdf(inputPdf);

            // Set the field "EmployeeID" to be hidden.
            // AnnotationFlags.Hidden makes the field invisible in the viewer.
            // Do NOT set NoExport, so the field value will still be submitted.
            bool success = editor.SetFieldAppearance("EmployeeID", AnnotationFlags.Hidden);
            if (!success)
            {
                Console.Error.WriteLine("Failed to set appearance for field 'EmployeeID'.");
            }

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Field 'EmployeeID' set to hidden (exportable) and saved to '{outputPdf}'.");
    }
}