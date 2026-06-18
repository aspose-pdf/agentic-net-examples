using System;
using System.IO;
using Aspose.Pdf.Facades;

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

        // Bind the PDF to FormEditor
        using (FormEditor editor = new FormEditor())
        {
            editor.BindPdf(inputPdf);

            // NOTE: FormEditor.SetFieldAttribute can only set predefined flags
            // (NoExport, ReadOnly, Required). It cannot assign arbitrary custom
            // attributes such as "data-id". Therefore a custom attribute cannot be
            // set via this method. Below is an example of setting a standard flag.
            bool success = editor.SetFieldAttribute(fieldName, PropertyFlag.NoExport);
            Console.WriteLine($"Set NoExport flag on '{fieldName}': {success}");

            // Save the modified PDF
            editor.Save(outputPdf);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPdf}'.");
    }
}