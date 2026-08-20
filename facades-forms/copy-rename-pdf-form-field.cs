using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";   // source PDF containing the original field
        const string outputPdf = "output.pdf";  // PDF that will receive the copied field

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        // FormEditor works with a source and a destination PDF.
        // It implements IDisposable, so wrap it in a using block.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // Copy the outer definition of the field "AddressBlock" to page 3 of the destination PDF.
            // This creates a new field on page 3 with the same name ("AddressBlock").
            formEditor.CopyOuterField(inputPdf, "AddressBlock", 3);

            // Rename the newly copied field to "BillingAddress".
            formEditor.RenameField("AddressBlock", "BillingAddress");

            // Persist the changes to the output file.
            formEditor.Save();
        }

        Console.WriteLine($"Field copied and renamed. Output saved to '{outputPdf}'.");
    }
}