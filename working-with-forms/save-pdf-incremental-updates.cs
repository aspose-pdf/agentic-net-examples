using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;
using Aspose.Pdf.Annotations;

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

        // Open the PDF with a writable stream (ReadWrite) to enable incremental saving
        using (FileStream fs = new FileStream(inputPath, FileMode.Open, FileAccess.ReadWrite))
        using (Document doc = new Document(fs))
        {
            // Example modification: set a value for a form field named "Name"
            // The field must be cast to the appropriate concrete type (e.g., TextBoxField) to set its value.
            if (doc.Form != null && doc.Form["Name"] != null)
            {
                if (doc.Form["Name"] is TextBoxField txtField)
                {
                    txtField.Value = "John Doe";
                }
                else if (doc.Form["Name"] is ComboBoxField comboField)
                {
                    comboField.Value = "John Doe"; // works for combo boxes as well
                }
                // Add other field type checks as needed.
            }

            // Incremental saving is automatically performed when the document is opened with a writable stream
            // and Save() is called without specifying a file name.
            doc.Save();

            // Optional check: does the document now have incremental updates?
            bool hasInc = doc.HasIncrementalUpdate();
            Console.WriteLine($"Incremental update saved: {hasInc}");
        }

        // Copy the updated file to a new location (optional, since the original file was modified in place)
        File.Copy(inputPath, outputPath, overwrite: true);
        Console.WriteLine($"Incrementally updated PDF saved as '{outputPath}'.");
    }
}
