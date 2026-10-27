using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "incremental_updated.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Create a copy that will be updated incrementally.
        File.Copy(inputPath, outputPath, true);

        // Open the PDF with a read/write stream – this enables incremental updates.
        using (var fs = new FileStream(outputPath, FileMode.Open, FileAccess.ReadWrite))
        using (var doc = new Document(fs))
        {
            // Retrieve the form field safely – the Form indexer returns a WidgetAnnotation,
            // so we cast it to Aspose.Pdf.Forms.Field before accessing field members.
            Field? nameField = doc.Form["Name"] as Field;
            if (nameField != null)
            {
                nameField.Value = "John Doe";
            }

            // Parameterless Save performs an incremental update when the document
            // was opened from a read/write stream.
            doc.Save();
        }

        Console.WriteLine($"PDF saved with incremental updates to '{outputPath}'.");
    }
}
