using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPdf  = "input.pdf";          // source PDF containing the form field
        const string outputPdf = "output_moved.pdf";   // destination PDF after moving the field
        const string fieldName = "myTextField";        // fully qualified name of the field to move

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // FormEditor works with two file names: source and destination.
        // It implements IDisposable, so we wrap it in a using block.
        using (FormEditor formEditor = new FormEditor(inputPdf, outputPdf))
        {
            // MoveField changes only the position of the field.
            // The coordinates are in points (1 inch = 72 points).
            // Example: move the field to a rectangle defined by (llx, lly, urx, ury).
            bool success = formEditor.MoveField(fieldName, 50f, 100f, 250f, 120f);

            if (!success)
            {
                Console.Error.WriteLine($"Failed to move field '{fieldName}'.");
            }

            // Persist the changes to the destination file.
            formEditor.Save();
        }

        Console.WriteLine($"Field '{fieldName}' has been moved and saved to '{outputPdf}'.");
    }
}