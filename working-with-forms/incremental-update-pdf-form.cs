using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_incremental.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF with a read/write stream – required for incremental updates
        using (FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.ReadWrite))
        {
            var doc = new Document(fs);

            // OPTIONAL: modify a form field to demonstrate an incremental change
            const string fieldName = "FieldName";
            if (doc.Form != null && doc.Form[fieldName] != null)
            {
                // Cast to a concrete field type that exposes the Value property
                if (doc.Form[fieldName] is TextBoxField textBox)
                {
                    textBox.Value = "New value";
                }
                else if (doc.Form[fieldName] is CheckboxField checkBox)
                {
                    checkBox.Checked = true;
                }
                // Additional field types (ComboBoxField, RadioButtonField, etc.) can be handled similarly
            }

            // Save the document. When the same stream is used for reading and writing,
            // Aspose.PDF automatically performs an incremental update.
            doc.Save(fs);

            // Verify that the document now contains incremental updates (if supported by the version)
            bool hasIncremental = doc.HasIncrementalUpdate();
            Console.WriteLine($"Incremental update applied: {hasIncremental}");
        }

        // Copy the updated file to a separate output path (if a distinct file is desired)
        File.Copy(inputPath, outputPath, overwrite: true);
        Console.WriteLine($"Incrementally updated PDF saved to '{outputPath}'.");
    }
}
