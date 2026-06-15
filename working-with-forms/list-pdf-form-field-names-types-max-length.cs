using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths of PDF files to analyze
        string[] pdfFiles = { "file1.pdf", "file2.pdf", "file3.pdf" };

        foreach (string pdfPath in pdfFiles)
        {
            if (!File.Exists(pdfPath))
            {
                Console.Error.WriteLine($"File not found: {pdfPath}");
                continue;
            }

            // Load each PDF inside a using block (lifecycle rule)
            using (Document doc = new Document(pdfPath))
            {
                Console.WriteLine($"--- Form fields in '{Path.GetFileName(pdfPath)}' ---");

                // Iterate over all form fields
                foreach (Field field in doc.Form.Fields)
                {
                    // Full field name (fallbacks if null)
                    string fieldName = field.FullName ?? field.Name ?? field.PartialName ?? "(unnamed)";

                    // Type of the field (class name)
                    string fieldType = field.GetType().Name;

                    // Character limit – only for text fields (MaxLen property)
                    int? maxLength = null;
                    if (field is TextBoxField txtField)
                    {
                        maxLength = txtField.MaxLen;
                    }

                    // Output the collected information
                    if (maxLength.HasValue)
                        Console.WriteLine($"Name: {fieldName}, Type: {fieldType}, Max Length: {maxLength.Value}");
                    else
                        Console.WriteLine($"Name: {fieldName}, Type: {fieldType}");
                }

                Console.WriteLine(); // Separate output for each file
            }
        }
    }
}