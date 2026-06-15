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
            // Access the form field named "EmployeeID"
            // Use the concrete field type (e.g., TextBoxField) instead of the generic WidgetAnnotation
            TextBoxField employeeIdField = doc.Form["EmployeeID"] as TextBoxField;

            if (employeeIdField == null)
            {
                Console.Error.WriteLine("Field 'EmployeeID' not found or is not a text box.");
            }
            else
            {
                // Set the field to read‑only so users cannot modify it
                employeeIdField.ReadOnly = true;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with read‑only EmployeeID field: {outputPath}");
    }
}