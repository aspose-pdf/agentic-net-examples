using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }

        // Initialize FormEditor with source PDF and destination PDF
        FormEditor formEditor = new FormEditor(sourcePath, outputPath);

        // Copy the outer definition of the field "AddressBlock" to a new field "BillingAddress" on page 3
        formEditor.CopyInnerField("AddressBlock", "BillingAddress", 3);

        // Persist the changes
        formEditor.Save();

        Console.WriteLine($"Field 'AddressBlock' copied as 'BillingAddress' on page 3. Saved to '{outputPath}'.");
    }
}