using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    // Simple data holder for report rows
    private class FieldInfoRecord
    {
        public string FileName { get; set; }
        public string FieldName { get; set; }
        public string FieldType { get; set; }
        public string CharLimit { get; set; }
    }

    static void Main()
    {
        // Directory containing PDF files to analyze
        const string pdfDirectory = @"C:\PdfFiles";

        if (!Directory.Exists(pdfDirectory))
        {
            Console.Error.WriteLine($"Directory not found: {pdfDirectory}");
            return;
        }

        // Gather all PDF files in the directory (non‑recursive)
        string[] pdfFiles = Directory.GetFiles(pdfDirectory, "*.pdf", SearchOption.TopDirectoryOnly);
        if (pdfFiles.Length == 0)
        {
            Console.WriteLine("No PDF files found.");
            return;
        }

        var report = new List<FieldInfoRecord>();

        foreach (string pdfPath in pdfFiles)
        {
            // Load each PDF using Aspose.Pdf Document inside a using block (lifecycle rule)
            using (Document doc = new Document(pdfPath))
            {
                // Iterate over the form fields collection (correct API)
                foreach (Field field in doc.Form.Fields)
                {
                    // Field name (PartialName is the proper property for FormField)
                    string name = field.PartialName ?? "(unnamed)";

                    // Field type (simple class name)
                    string type = field.GetType().Name;

                    // Character limit – not all field types expose MaxLen
                    string charLimit = "N/A";
                    PropertyInfo maxLenProp = field.GetType().GetProperty("MaxLen", BindingFlags.Public | BindingFlags.Instance);
                    if (maxLenProp != null && maxLenProp.PropertyType == typeof(int))
                    {
                        object value = maxLenProp.GetValue(field);
                        if (value != null)
                        {
                            charLimit = value.ToString();
                        }
                    }

                    report.Add(new FieldInfoRecord
                    {
                        FileName = Path.GetFileName(pdfPath),
                        FieldName = name,
                        FieldType = type,
                        CharLimit = charLimit
                    });
                }
            }
        }

        // Output report to console (could be redirected to a file if needed)
        Console.WriteLine("PDF Form Fields Report");
        Console.WriteLine("File\tField Name\tField Type\tCharacter Limit");
        foreach (var rec in report)
        {
            Console.WriteLine($"{rec.FileName}\t{rec.FieldName}\t{rec.FieldType}\t{rec.CharLimit}");
        }
    }
}
