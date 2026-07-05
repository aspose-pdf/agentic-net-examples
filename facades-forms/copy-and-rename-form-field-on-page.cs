using System;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePdf = "input.pdf";
        const string outputPdf = "output.pdf";

        if (!System.IO.File.Exists(sourcePdf))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePdf}");
            return;
        }

        // Initialize FormEditor with source and destination PDFs
        using (FormEditor formEditor = new FormEditor(sourcePdf, outputPdf))
        {
            // Copy the outer definition of "AddressBlock" to page 3 of the target PDF
            formEditor.CopyOuterField(sourcePdf, "AddressBlock", 3);

            // Rename the newly copied field to "BillingAddress"
            formEditor.RenameField("AddressBlock", "BillingAddress");

            // Persist changes
            formEditor.Save();
        }

        Console.WriteLine($"Field copied and renamed. Output saved to '{outputPdf}'.");
    }
}