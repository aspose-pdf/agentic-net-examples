using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";               // source PDF
        const string outputPath = "output_incremental.pdf"; // file with incremental updates

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Copy the original file so the source remains untouched.
        File.Copy(inputPath, outputPath, overwrite: true);

        // Open the copy with read/write access – required for incremental updates.
        using (FileStream stream = new FileStream(outputPath, FileMode.Open, FileAccess.ReadWrite))
        using (Document doc = new Document(stream))
        {
            // OPTIONAL: make a change (e.g., update the first form field)
            if (doc.Form != null && doc.Form.Count > 0)
            {
                // The Form collection indexer returns a WidgetAnnotation.
                // Cast it to Aspose.Pdf.Forms.Field to access the Value property.
                Field? field = doc.Form[0] as Field;
                if (field != null)
                {
                    field.Value = "Updated value";
                }
            }

            // Save incrementally – this keeps all previous revisions inside the same file.
            // The parameter‑less Save() overload performs an incremental update when the document
            // was opened from a read/write stream.
            doc.Save();
        }

        Console.WriteLine("PDF saved with incremental updates.");
    }
}
