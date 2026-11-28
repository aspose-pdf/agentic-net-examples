using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output3.pdf";
        const string fieldName = "CustomerName";

        // Verify the source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: File not found – {inputPath}");
            return;
        }

        // FormEditor implements IDisposable; use a using block for proper cleanup
        using (FormEditor editor = new FormEditor())
        {
            // Bind the PDF file to the editor
            editor.BindPdf(inputPath);

            // Remove the specified form field if it exists
            editor.RemoveField(fieldName);

            // Save the modified PDF to the output file
            editor.Save(outputPath);
        }

        Console.WriteLine($"Field \"{fieldName}\" removed. Output saved to \"{outputPath}\".");
    }
}
