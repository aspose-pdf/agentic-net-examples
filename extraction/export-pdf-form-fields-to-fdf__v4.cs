using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "FormD.pdf";
        const string outputFdf = "FormD_fields.fdf";

        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPdf))
            {
                // Enumerate all form fields and write basic info to the console
                foreach (Field field in doc.Form.Fields)
                {
                    string name = field.FullName ?? "(no name)";
                    string type = field.GetType().Name;
                    string value = field.Value?.ToString() ?? "(null)";
                    Console.WriteLine($"Field: {name}, Type: {type}, Value: {value}");
                }

                // Export the form data to a simple FDF file using only core APIs
                ExportFormDataToFdf(doc, outputFdf);
                Console.WriteLine($"Form data exported to: {outputFdf}");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Creates a minimal FDF representation of the form fields.
    /// This implementation uses only the core Aspose.Pdf APIs and does not rely on the Facades namespace.
    /// </summary>
    /// <param name="doc">The PDF document containing the form.</param>
    /// <param name="fdfPath">The path where the FDF file will be written.</param>
    private static void ExportFormDataToFdf(Document doc, string fdfPath)
    {
        // Basic FDF header (compatible with most PDF viewers)
        var sb = new StringBuilder();
        sb.AppendLine("%FDF-1.2");
        sb.AppendLine("%âãÏÓ");
        sb.AppendLine("1 0 obj");
        sb.AppendLine("<< /FDF << /Fields [");

        foreach (Field field in doc.Form.Fields)
        {
            string name = EscapeString(field.FullName ?? string.Empty);
            string value = EscapeString(field.Value?.ToString() ?? string.Empty);
            sb.AppendLine($"<< /T ({name}) /V ({value}) >>");
        }

        sb.AppendLine("] >> >>");
        sb.AppendLine("endobj");
        sb.AppendLine("trailer");
        sb.AppendLine("<< /Root 1 0 R >>");
        sb.AppendLine("%%EOF");

        File.WriteAllText(fdfPath, sb.ToString(), Encoding.ASCII);
    }

    /// <summary>
    /// Escapes backslashes and parentheses which have special meaning in PDF strings.
    /// </summary>
    private static string EscapeString(string input)
    {
        if (string.IsNullOrEmpty(input))
            return string.Empty;
        return input.Replace("\\", "\\\\").Replace("(", "\\(").Replace(")", "\\)");
    }
}
