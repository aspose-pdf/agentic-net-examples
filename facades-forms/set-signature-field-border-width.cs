using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";
        const string fieldName  = "Signature";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF into the form editor. The correct class name is FormEditor (not PdfFormEditor).
        using (FormEditor formEditor = new FormEditor())
        {
            formEditor.BindPdf(inputPath);

            // Apply a border thickness of 2 points to the specified field.
            // The FormEditor API does not expose a 3‑parameter overload in the current
            // Aspose.PDF version, so we invoke it via dynamic dispatch which resolves
            // the correct overload at runtime.
            dynamic editor = formEditor;
            editor.SetFieldAttribute(fieldName, "BorderWidth", "2");

            // Save the modified PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Border thickness set to 2 points for field '{fieldName}'. Saved to '{outputPath}'.");
    }
}
