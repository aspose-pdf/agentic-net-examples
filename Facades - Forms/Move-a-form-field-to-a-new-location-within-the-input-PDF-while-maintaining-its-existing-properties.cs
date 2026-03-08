using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        // Paths for the source and the resulting PDF
        const string inputPdf = "input.pdf";
        const string outputPdf = "output.pdf";

        // Fully qualified name of the field to be moved
        const string fieldName = "myTextField";

        // New position for the field (lower‑left and upper‑right corners)
        float llx = 100f; // lower‑left X
        float lly = 200f; // lower‑left Y
        float urx = 300f; // upper‑right X
        float ury = 250f; // upper‑right Y

        // Verify that the source file exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"Input file not found: {inputPdf}");
            return;
        }

        // Create a FormEditor instance and load the PDF
        FormEditor formEditor = new FormEditor();
        formEditor.BindPdf(inputPdf);

        // Move the specified field to the new rectangle.
        // MoveField returns true if the operation succeeded.
        bool moved = formEditor.MoveField(fieldName, llx, lly, urx, ury);
        if (!moved)
        {
            Console.Error.WriteLine($"Failed to move field '{fieldName}'.");
        }

        // Save the modified document to a new file.
        formEditor.Save(outputPdf);
        Console.WriteLine($"Field '{fieldName}' moved and saved to '{outputPdf}'.");
    }
}