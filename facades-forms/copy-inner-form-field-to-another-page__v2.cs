using System;
using System.IO;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "source.pdf";
        const string outputPath = "result.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // FormEditor works with the PDF structure; it does not implement IDisposable.
        var editor = new FormEditor();

        // Load the PDF into the editor.
        editor.BindPdf(inputPath);

        // Copy the inner field "LastName" to a new field "LastNameCopy" on page 2.
        // The correct API is CopyInnerField(sourceName, destinationName, targetPage).
        editor.CopyInnerField("LastName", "LastNameCopy", 2);

        // Persist the changes.
        editor.Save(outputPath);

        Console.WriteLine($"Field copied and saved to '{outputPath}'.");
    }
}
