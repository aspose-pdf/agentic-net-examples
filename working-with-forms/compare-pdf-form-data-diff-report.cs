using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string pdfPath1 = "form1.pdf";
        const string pdfPath2 = "form2.pdf";
        const string reportPath = "diffReport.txt";

        if (!File.Exists(pdfPath1) || !File.Exists(pdfPath2))
        {
            Console.Error.WriteLine("One or both PDF files not found.");
            return;
        }

        try
        {
            // Load both PDFs inside using blocks for deterministic disposal
            using (Document doc1 = new Document(pdfPath1))
            using (Document doc2 = new Document(pdfPath2))
            using (StreamWriter report = new StreamWriter(reportPath, false))
            {
                // Write header for the report
                report.WriteLine("Form Data Comparison Report");
                report.WriteLine($"File 1: {pdfPath1}");
                report.WriteLine($"File 2: {pdfPath2}");
                report.WriteLine(new string('=', 50));
                report.WriteLine();

                // Compare fields that exist in the first document
                foreach (Field field1 in doc1.Form.Fields)
                {
                    string fieldName = field1.FullName ?? field1.Name ?? string.Empty;

                    // Try to find the same field in the second document (cast required)
                    Field field2 = doc2.Form[fieldName] as Field;

                    if (field2 == null)
                    {
                        report.WriteLine($"Field \"{fieldName}\" exists only in the first PDF.");
                        continue;
                    }

                    // Retrieve the string representation of each field's value
                    string value1 = field1.Value?.ToString() ?? string.Empty;
                    string value2 = field2.Value?.ToString() ?? string.Empty;

                    if (!value1.Equals(value2, StringComparison.Ordinal))
                    {
                        report.WriteLine($"Mismatch in field \"{fieldName}\":");
                        report.WriteLine($"    PDF 1 value: \"{value1}\"");
                        report.WriteLine($"    PDF 2 value: \"{value2}\"");
                        report.WriteLine();
                    }
                }

                // Find fields that exist only in the second document
                foreach (Field field2 in doc2.Form.Fields)
                {
                    string fieldName = field2.FullName ?? field2.Name ?? string.Empty;
                    if (doc1.Form[fieldName] as Field == null)
                    {
                        report.WriteLine($"Field \"{fieldName}\" exists only in the second PDF.");
                    }
                }

                Console.WriteLine($"Diff report generated at \"{reportPath}\".");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error during comparison: {ex.Message}");
        }
    }
}
