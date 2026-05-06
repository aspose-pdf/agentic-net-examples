using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "input.pdf";          // original PDF containing "AddressBlock"
        const string targetPdf = "output.pdf";         // PDF that will receive the new field

        if (!File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // FormEditor works with a source and a destination PDF.
        // It implements IDisposable, so wrap it in a using block.
        using (FormEditor formEditor = new FormEditor(sourcePdf, targetPdf))
        {
            // Copy the outer definition of the field "AddressBlock" to page 3.
            // This creates a new field on page 3 with the same name.
            formEditor.CopyOuterField(sourcePdf, "AddressBlock", 3);

            // Rename the newly copied field to "BillingAddress".
            // After copying, both the original and the copy have the same name.
            // Rename will affect the most recently added field with that name.
            formEditor.RenameField("AddressBlock", "BillingAddress");

            // Persist changes to the target PDF.
            formEditor.Save();
        }

        Console.WriteLine($"Field copied and renamed. Result saved to '{targetPdf}'.");
    }
}