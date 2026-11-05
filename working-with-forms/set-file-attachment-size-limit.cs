using System;
using System.IO;
using System.Linq;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";   // source PDF containing the file attachment field
        const string outputPath = "output.pdf"; // PDF after applying the constraint
        const string fieldName = "fileField";   // name of the attached file field to constrain

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document using the core Document API.
        Document doc = new Document(inputPath);

        // Ensure the form exists.
        if (doc.Form == null)
        {
            Console.Error.WriteLine("The PDF does not contain a form.");
            return;
        }

        // Verify that the specified field exists in the form.
        bool fieldExists = doc.Form.Fields != null &&
                           doc.Form.Fields.Any(f =>
                               string.Equals(f.Name, fieldName, StringComparison.Ordinal) ||
                               string.Equals(f.FullName, fieldName, StringComparison.Ordinal));
        if (!fieldExists)
        {
            Console.Error.WriteLine($"Form field '{fieldName}' not found in the PDF.");
            return;
        }

        // Retrieve the field (no direct cast to FileAttachmentField to avoid version‑specific type issues).
        var field = doc.Form[fieldName];
        if (field == null)
        {
            Console.Error.WriteLine($"Field '{fieldName}' could not be retrieved.");
            return;
        }

        // Attempt to set the maximum allowed file size (2 MB) via reflection – works for FileAttachmentField and safely skips other field types.
        var maxSizeProp = field.GetType().GetProperty("MaxFileSize");
        if (maxSizeProp != null && maxSizeProp.CanWrite)
        {
            maxSizeProp.SetValue(field, 2 * 1024 * 1024); // 2 megabytes
        }
        else
        {
            Console.Error.WriteLine($"Field '{fieldName}' does not support a MaxFileSize constraint.");
            return;
        }

        // Save the modified PDF.
        doc.Save(outputPath);

        Console.WriteLine($"Maximum file size constraint applied to field '{fieldName}'. Saved as '{outputPath}'.");
    }
}
