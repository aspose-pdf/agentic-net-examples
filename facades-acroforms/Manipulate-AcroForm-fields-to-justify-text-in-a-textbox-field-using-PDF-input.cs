using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Input PDF containing the AcroForm
        const string inputPath = "input.pdf";
        // Output PDF with justified text in the textbox field
        const string outputPath = "output.pdf";
        // Fully qualified name of the textbox field to modify
        const string fieldName = "TextBox1"; // adjust to your field name

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize FormEditor with source and destination files
        using (FormEditor formEditor = new FormEditor(inputPath, outputPath))
        {
            // Horizontal alignment: 0 = Left, 1 = Center, 2 = Right
            // Set to Center (justify) for the specified textbox field
            formEditor.SetFieldAlignment(fieldName, 1);

            // Optional: vertical alignment (0 = Top, 1 = Middle, 2 = Bottom)
            // Set to Middle for better visual balance
            formEditor.SetFieldAlignmentV(fieldName, 1);

            // Persist changes to the output PDF
            formEditor.Save();
        }

        Console.WriteLine($"Textbox field '{fieldName}' justified and saved to '{outputPath}'.");
    }
}