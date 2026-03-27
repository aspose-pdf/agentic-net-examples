using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";
        const string secondPath = "second.pdf";
        const string editedPath = "edited.pdf";
        const string outputPath = "merged.pdf";

        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine("Source file not found: " + sourcePath);
            return;
        }

        if (!File.Exists(secondPath))
        {
            Console.Error.WriteLine("Second file not found: " + secondPath);
            return;
        }

        // Load the source PDF and delete a page (if it exists)
        using (Document sourceDoc = new Document(sourcePath))
        {
            if (sourceDoc.Pages.Count >= 2)
            {
                sourceDoc.Pages.Delete(2);
            }

            sourceDoc.Save(editedPath);
        }

        // Concatenate the edited PDF with the second PDF using PdfFileEditor
        PdfFileEditor editor = new PdfFileEditor();
        bool success = editor.Concatenate(editedPath, secondPath, outputPath);
        if (success)
        {
            Console.WriteLine("Concatenation succeeded. Result saved as " + outputPath);
        }
        else
        {
            Console.Error.WriteLine("Concatenation failed.");
        }

        // Remove the temporary edited file
        if (File.Exists(editedPath))
        {
            try
            {
                File.Delete(editedPath);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Could not delete temporary file: " + ex.Message);
            }
        }
    }
}