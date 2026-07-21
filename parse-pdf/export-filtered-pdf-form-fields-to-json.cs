using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPdfPath = "input.pdf";          // source PDF with form fields
        const string outputJsonPath = "filtered_fields.json"; // output JSON file
        const string fieldNamePrefix = "Customer_";       // prefix to filter field names

        if (!File.Exists(inputPdfPath))
        {
            Console.Error.WriteLine($"File not found: {inputPdfPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document pdfDoc = new Document(inputPdfPath))
        {
            // Collect fields whose names start with the specified prefix
            List<Field> matchingFields = new List<Field>();
            foreach (Field field in pdfDoc.Form.Fields)
            {
                if (!string.IsNullOrEmpty(field.Name) &&
                    field.Name.StartsWith(fieldNamePrefix, StringComparison.Ordinal))
                {
                    matchingFields.Add(field);
                }
            }

            // Export the filtered fields to a single JSON array
            using (FileStream fs = new FileStream(outputJsonPath, FileMode.Create, FileAccess.Write))
            using (StreamWriter writer = new StreamWriter(fs, Encoding.UTF8))
            {
                writer.WriteLine("["); // start of JSON array

                for (int i = 0; i < matchingFields.Count; i++)
                {
                    Field field = matchingFields[i];

                    // Export the individual field value to a temporary memory stream
                    using (MemoryStream tempStream = new MemoryStream())
                    {
                        // ExportValueToJson writes a JSON object representing the field
                        field.ExportValueToJson(tempStream, indented: true);
                        string fieldJson = Encoding.UTF8.GetString(tempStream.ToArray());

                        writer.Write(fieldJson);

                        // Add a comma between objects, but not after the last one
                        if (i < matchingFields.Count - 1)
                            writer.WriteLine(",");
                        else
                            writer.WriteLine();
                    }
                }

                writer.WriteLine("]"); // end of JSON array
            }

            Console.WriteLine($"Filtered fields exported to '{outputJsonPath}'.");
        }
    }
}