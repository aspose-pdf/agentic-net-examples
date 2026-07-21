using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF file into a memory stream
        using (FileStream fileStream = File.OpenRead(inputPath))
        using (MemoryStream memoryStream = new MemoryStream())
        {
            fileStream.CopyTo(memoryStream);
            memoryStream.Position = 0; // reset to beginning

            // Bind the memory stream to PdfPageEditor for in‑memory manipulation
            using (PdfPageEditor editor = new PdfPageEditor())
            {
                editor.BindPdf(memoryStream);

                // Example manipulation: rotate all pages 90 degrees
                editor.Rotation = 90;          // valid values: 0, 90, 180, 270
                editor.ProcessPages = null;    // null processes every page

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the edited PDF to a file
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}