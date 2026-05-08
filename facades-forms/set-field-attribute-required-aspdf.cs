using System;
using System.IO;
using Aspose.Pdf.Facades;
using Aspose.Pdf.Facades; // FormEditor, PropertyFlag
using Aspose.Pdf;          // Document (if needed)

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";
        const string outputPdf = "output.pdf";
        const string fieldName = "OrderNumber";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // Bind the PDF to a FormEditor instance.
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPdf);

            // NOTE:
            // FormEditor.SetFieldAttribute can only set predefined flags
            // (NoExport, ReadOnly, Required) via the PropertyFlag enum.
            // It does NOT support arbitrary custom attributes such as
            // "data-id". Therefore we cannot assign a custom attribute
            // directly using this API. As a placeholder, we demonstrate
            // setting the Required flag, which is the closest supported
            // operation.
            bool result = editor.SetFieldAttribute(fieldName, PropertyFlag.Required);

            if (!result)
            {
                Console.Error.WriteLine($"Failed to set attribute on field '{fieldName}'.");
            }
            else
            {
                Console.WriteLine($"Required attribute set on field '{fieldName}'.");
            }

            // Save the modified PDF.
            editor.Save(outputPdf);
        }

        Console.WriteLine($"PDF saved to '{outputPdf}'.");
    }
}