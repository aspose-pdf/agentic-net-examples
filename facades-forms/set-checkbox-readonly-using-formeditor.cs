using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "document.pdf";
        const string outputPath = "output.pdf";
        const string checkBoxName = "CheckBox1"; // replace with the actual field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor is a Facades class that works with form fields.
        // It implements IDisposable, so wrap it in a using block.
        using (FormEditor formEditor = new FormEditor())
        {
            // Load the PDF into the editor.
            formEditor.BindPdf(inputPath);

            // Set the ReadOnly attribute on the specified checkbox.
            // The correct method is SetFieldAttribute with the PropertyFlag enum.
            formEditor.SetFieldAttribute(checkBoxName, PropertyFlag.ReadOnly);

            // Save the modified PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Checkbox '{checkBoxName}' set to read‑only. Output saved to '{outputPath}'.");
    }
}
