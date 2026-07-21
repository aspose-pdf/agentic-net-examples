using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string outputPath = "edited.pdf";
        const string password = "userpass";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the encrypted PDF by providing the user/owner password
        using (Document doc = new Document(inputPath, password))
        {
            // Bind the loaded document to PdfPageEditor
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Example manipulation: rotate all pages 90 degrees
                editor.Rotation = 90;

                // Apply the changes to the document
                editor.ApplyChanges();

                // Save the modified PDF
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}