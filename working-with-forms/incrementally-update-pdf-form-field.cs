using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // needed for Field type

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_incremental.pdf";

        // Verify source PDF exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Source file not found: {inputPath}");
            return;
        }

        // Create a copy that will receive incremental updates
        File.Copy(inputPath, outputPath, overwrite: true);

        // Open the copy with read/write access – required for incremental saving
        using (FileStream stream = new FileStream(outputPath, FileMode.Open, FileAccess.ReadWrite))
        using (Document doc = new Document(stream))
        {
            // Example modification: update a form field if it exists
            Field field = doc.Form["SampleField"] as Field; // use Field, not WidgetAnnotation
            if (field != null)
            {
                field.Value = "Updated value";
            }

            // Incremental save – preserves previous revisions of the PDF (including form data)
            // When a Document is opened from a read/write stream, the parameter‑less Save() performs an incremental update.
            doc.Save();
        }

        Console.WriteLine($"Incrementally updated PDF saved to '{outputPath}'.");
    }
}
