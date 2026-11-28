using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "output003.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor does not implement IDisposable, so we instantiate it directly.
        FormEditor formEditor = new FormEditor();

        // Bind the source PDF document.
        formEditor.BindPdf(inputPath);

        // Add a push‑button field named "SubmitOrder" on page 3.
        // Lower‑left corner at (200, 300), upper‑right corner at (300, 340).
        formEditor.AddField(FieldType.PushButton, "SubmitOrder", 3, 200f, 300f, 300f, 340f);

        // Save the modified PDF to the specified output file.
        formEditor.Save(outputPath);
        formEditor.Close();

        Console.WriteLine($"Button added and saved to '{outputPath}'.");
    }
}
