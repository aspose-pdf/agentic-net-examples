using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source PDF (contains the original field) and the output PDF.
        const string sourcePdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Ensure the source file exists.
        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // FormEditor works with two files: the source (to read from) and the destination (to write to).
        // The constructor does not implement IDisposable directly, but FormEditor inherits SaveableFacade,
        // which implements IDisposable, so we wrap it in a using block for deterministic disposal.
        using (FormEditor formEditor = new FormEditor(sourcePdf, outputPdf))
        {
            // Copy the outer definition of the field "AddressBlock" to a new field named
            // "BillingAddress" on page 3 of the destination PDF.
            // CopyInnerField creates a copy of an existing field with a new name.
            // Page numbers are 1‑based in Aspose.Pdf, so page 3 refers to the third page.
            formEditor.CopyInnerField("AddressBlock", "BillingAddress", 3);

            // Persist the changes to the output file.
            formEditor.Save();
        }

        Console.WriteLine($"Field copied successfully. Output saved to '{outputPdf}'.");
    }
}