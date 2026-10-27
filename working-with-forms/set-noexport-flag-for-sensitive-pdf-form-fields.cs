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
        const string outputPath = "output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block (document-disposal-with-using rule)
        using (Document doc = new Document(inputPath))
        {
            // Iterate over all form fields in the document
            foreach (Field field in doc.Form)
            {
                // Mark fields that are considered sensitive
                if (IsSensitive(field))
                {
                    // Preserve existing flags and add the NoExport flag (value 0x10).
                    // AnnotationFlags does not expose NoExport, so we manipulate the underlying integer value.
                    field.Flags = (AnnotationFlags)((int)field.Flags | 0x10);
                }
            }

            // Save the updated PDF (still within the using block)
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }

    // Simple heuristic to decide if a field is sensitive
    static bool IsSensitive(Field field)
    {
        string name = field?.FullName?.ToLowerInvariant() ?? string.Empty;
        return name.Contains("ssn") ||
               name.Contains("creditcard") ||
               name.Contains("password") ||
               name.Contains("sensitive");
    }
}
