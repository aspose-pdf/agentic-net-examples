using System;
using Aspose.Pdf.Facades;          // FormEditor, PropertyFlag
using Aspose.Pdf;                 // PropertyFlag enum resides here

class Program
{
    static void Main()
    {
        const string inputPdf  = "FormWithAgreement.pdf";   // source PDF containing the field
        const string outputPdf = "FormWithAgreement_Required.pdf";

        // Ensure the source file exists before proceeding
        if (!System.IO.File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor implements IDisposable; use a using block for deterministic cleanup
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Mark the field named "Agreement" as required.
            // The Required flag also triggers the standard asterisk indicator in PDF viewers.
            bool success = formEditor.SetFieldAttribute("Agreement", PropertyFlag.Required);

            if (!success)
            {
                Console.Error.WriteLine("Failed to set the Required attribute on the 'Agreement' field.");
            }

            // Save writes the modified PDF to the output path specified in the constructor.
            formEditor.Save();
        }

        Console.WriteLine($"Processed PDF saved to '{outputPdf}'.");
    }
}