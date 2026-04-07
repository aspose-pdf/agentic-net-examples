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

        // Load the PDF, set the EmployeeID field to read‑only, and save.
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the form field named "EmployeeID".
            // The Form indexer returns a WidgetAnnotation, so cast it to Field.
            Field employeeIdField = doc.Form["EmployeeID"] as Field;
            if (employeeIdField != null)
            {
                // Prevent user modifications after generation.
                employeeIdField.ReadOnly = true;
            }
            else
            {
                Console.WriteLine("Field 'EmployeeID' not found or is not a form field.");
            }

            // Persist the changes.
            doc.Save(outputPath);
        }

        Console.WriteLine($"Modified PDF saved to '{outputPath}'.");
    }
}
