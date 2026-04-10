using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string inputPath  = "encrypted_input.pdf";
        const string outputPath = "edited_output.pdf";
        const string password   = "userOrOwnerPassword";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the encrypted PDF with the provided password.
        // Document constructor accepts a password for encrypted files.
        using (Document doc = new Document(inputPath, password))
        {
            // Bind the opened document to PdfPageEditor.
            // Using the constructor that accepts a Document allows us to work with the encrypted PDF.
            using (PdfPageEditor editor = new PdfPageEditor(doc))
            {
                // Example manipulation: rotate all pages by 90 degrees.
                editor.Rotation = 90;               // valid values: 0, 90, 180, 270
                editor.ApplyChanges();              // apply the rotation to the pages

                // Save the edited PDF. PdfPageEditor.Save(string) writes the result to the specified file.
                editor.Save(outputPath);
            }
        }

        Console.WriteLine($"Edited PDF saved to '{outputPath}'.");
    }
}