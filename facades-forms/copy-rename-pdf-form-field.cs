using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Bind the PDF to the form editor facade
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Copy the outer definition of the field "AddressBlock" to a new field "BillingAddress"
            // on page 3 (pages are 1‑based in Aspose.Pdf)
            formEditor.CopyOuterField("AddressBlock", "BillingAddress", 3);

            // Save the modified PDF
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field copied successfully. Output saved to '{outputPath}'.");
    }
}
