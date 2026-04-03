using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

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

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field named "EmployeeID"
            // The Form indexer returns a WidgetAnnotation; cast it to Field to access form‑specific members.
            Field employeeField = doc.Form["EmployeeID"] as Field;
            if (employeeField != null)
            {
                // Make the field read‑only to prevent user edits
                employeeField.ReadOnly = true;
            }
            else
            {
                Console.WriteLine("Field 'EmployeeID' not found in the document.");
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with read‑only EmployeeID field to '{outputPath}'.");
    }
}
