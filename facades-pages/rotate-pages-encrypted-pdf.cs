using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath = "encrypted.pdf";
        const string userPassword = "userpass";
        const string outputPath = "modified.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the encrypted PDF using the user (or owner) password.
        using (Document doc = new Document(inputPath, userPassword))
        {
            // Bind the opened document to PdfPageEditor.
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Example manipulation: rotate all pages 90 degrees.
                editor.Rotation = 90; // Valid values: 0, 90, 180, 270.

                // Apply the changes to the document.
                editor.ApplyChanges();

                // Save the edited PDF.
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}