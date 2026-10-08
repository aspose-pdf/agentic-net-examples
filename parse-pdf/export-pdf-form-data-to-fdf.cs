using System;
using System.IO;
using System.Text;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string fdfPath = "output.fdf";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"PDF not found: {pdfPath}");
            return;
        }

        // Load the PDF document; using ensures deterministic disposal.
        using (Document doc = new Document(pdfPath))
        {
            // Verify that the PDF contains a form.
            if (doc.Form == null || doc.Form.Count == 0)
            {
                Console.WriteLine("The PDF does not contain any form fields.");
                return;
            }

            // Create a FileStream for the FDF output; using will close the stream.
            using (FileStream fdfStream = new FileStream(fdfPath, FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fdfStream, Encoding.ASCII))
            {
                // Write a minimal FDF structure manually because the core API does not expose ExportFdf.
                writer.WriteLine("%FDF-1.2");
                writer.WriteLine("1 0 obj");
                writer.WriteLine("<<");
                writer.WriteLine("/FDF << /Fields [");

                foreach (Field field in doc.Form.Fields)
                {
                    string name = field.Name ?? field.FullName ?? string.Empty;
                    string value = field.Value?.ToString() ?? string.Empty;
                    // Escape parentheses in name/value according to PDF string rules.
                    name = name.Replace("(", "\\(").Replace(")", "\\)");
                    value = value.Replace("(", "\\(").Replace(")", "\\)");
                    writer.WriteLine($"<< /T ({name}) /V ({value}) >>");
                }

                writer.WriteLine("] >> >>");
                writer.WriteLine("endobj");
                writer.WriteLine("trailer << /Root 1 0 R >>");
                writer.WriteLine("%%EOF");
                writer.Flush(); // Ensure all data is written before the stream is closed.
            }
        }

        Console.WriteLine($"FDF data successfully written to '{fdfPath}'.");
    }
}
