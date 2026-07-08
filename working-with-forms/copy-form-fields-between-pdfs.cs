using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string sourcePath = "source.pdf";      // PDF containing the original form fields
        const string destinationPath = "destination.pdf"; // PDF that will receive the fields
        const string outputPath = "output.pdf";      // Resulting PDF after copying

        // Verify that both input files exist before proceeding.
        if (!File.Exists(sourcePath))
        {
            Console.Error.WriteLine($"Source file not found: {sourcePath}");
            return;
        }
        if (!File.Exists(destinationPath))
        {
            Console.Error.WriteLine($"Destination file not found: {destinationPath}");
            return;
        }

        // Load the source and destination PDFs using the core Document API.
        Document srcDoc = new Document(sourcePath);
        Document dstDoc = new Document(destinationPath);

        // Copy all form fields from the source PDF to the destination PDF.
        // Fields are cloned to avoid linking the original document objects.
        foreach (Field srcField in srcDoc.Form.Fields)
        {
            // Clone the field so it can be added to another document.
            Field clonedField = (Field)srcField.Clone();
            dstDoc.Form.Add(clonedField);
        }

        // Save the modified destination PDF to the specified output file.
        dstDoc.Save(outputPath);

        Console.WriteLine($"Form fields copied successfully. Output saved to '{outputPath}'.");
    }
}
