using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create the FormEditor facade (full namespace required)
        FormEditor formEditor = new FormEditor();

        // Load the source PDF
        formEditor.BindPdf(inputPath);

        // Aspose.Pdf.Facades.FormEditor.SetFieldAttribute only supports two parameters
        // (field name and a PropertyFlag). To store a custom attribute like "data-id",
        // we can embed a small JavaScript snippet that sets the attribute at runtime.
        // This is the recommended approach when a true custom‑attribute overload is not available.
        formEditor.SetFieldScript(
            "OrderNumber",
            "this.setAttribute('data-id', '12345');"
        );

        // Save the modified PDF
        formEditor.Save(outputPath);

        Console.WriteLine($"Custom attribute script set and saved to '{outputPath}'.");
    }
}
