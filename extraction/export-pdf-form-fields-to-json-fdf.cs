using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "FormF.pdf";
        const string outputFdf = "FormF_fields.fdf"; // will contain JSON data (core API does not support native FDF export)

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Enumerate all form fields and output basic info
                if (doc.Form != null && doc.Form.Fields != null)
                {
                    foreach (Field field in doc.Form.Fields)
                    {
                        string name = field.FullName ?? field.Name ?? "<unnamed>";
                        Console.WriteLine($"Field name: {name}, Type: {field.GetType().Name}");
                    }
                }
                else
                {
                    Console.WriteLine("No form fields found in the document.");
                }

                // Export the form field data to a JSON stream and save it with an .fdf extension
                using (FileStream fdfStream = new FileStream(outputFdf, FileMode.Create, FileAccess.Write))
                {
                    doc.Form.ExportToJson(fdfStream);
                }

                Console.WriteLine($"Form data exported to '{outputFdf}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
