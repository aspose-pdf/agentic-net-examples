using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdf = "input.pdf";
        const string outputTxt = "empty_fields.txt";

        // Verify input PDF exists
        if (!File.Exists(inputPdf))
        {
            Console.Error.WriteLine($"File not found: {inputPdf}");
            return;
        }

        try
        {
            // Document disposal with using (lifecycle rule)
            using (Document doc = new Document(inputPdf))
            {
                // Check if the PDF contains a form with fields
                if (doc.Form == null || doc.Form.Count == 0)
                {
                    Console.WriteLine("No form fields found in the PDF.");
                    return;
                }

                // Create/overwrite the output TXT file
                using (StreamWriter writer = new StreamWriter(outputTxt, false))
                {
                    // Iterate over all form fields using the Forms API
                    foreach (Field field in doc.Form.Fields)
                    {
                        // Field value may be null; treat null or empty as "empty"
                        string value = field.Value?.ToString() ?? string.Empty;

                        if (string.IsNullOrEmpty(value))
                        {
                            // Write the field's full name (or name if full name is unavailable) to the TXT file
                            writer.WriteLine(field.FullName ?? field.Name);
                        }
                    }
                }

                Console.WriteLine($"Empty form fields exported to '{outputTxt}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
