using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "source.pdf";
        const string outputPath = "moved1.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // FormEditor is a Facades class used for form field manipulation.
        // It implements IDisposable, so wrap it in a using block.
        using (FormEditor formEditor = new FormEditor())
        {
            // Bind the PDF document to the editor.
            formEditor.BindPdf(inputPath);

            // Move the field named "FirstName" to the rectangle (100,150)-(200,250).
            // Coordinates are in points; lower‑left (llx,lly) and upper‑right (urx,ury).
            formEditor.MoveField("FirstName", 100, 150, 200, 250);

            // Save the modified PDF.
            formEditor.Save(outputPath);
        }

        Console.WriteLine($"Field moved and saved to '{outputPath}'.");
    }
}