using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "source.pdf";
        const string outputPath = "updated.pdf";
        const string sourceField = "Phone";
        const string destField   = "PhoneCopy";
        const int    destPage    = 4; // page numbers are 1‑based

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor does not implement IDisposable, so we manage its lifetime manually.
        FormEditor formEditor = new FormEditor();

        try
        {
            // Load the PDF into the editor.
            formEditor.BindPdf(inputPath);

            // Copy the existing field "Phone" to a new field "PhoneCopy" on page 4.
            // Use CopyInnerField – the correct method provided by FormEditor.
            formEditor.CopyInnerField(sourceField, destField, destPage);

            // Save the modified document.
            formEditor.Save(outputPath);
        }
        finally
        {
            // Release any resources held by the editor.
            formEditor.Close();
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
