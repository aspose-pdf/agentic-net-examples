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
            Field employeeIdField = doc.Form["EmployeeID"] as Field;
            if (employeeIdField != null)
            {
                // Make the field read‑only to prevent user edits
                employeeIdField.ReadOnly = true;
            }
            else
            {
                Console.Error.WriteLine("Field 'EmployeeID' not found.");
            }

            // Save the updated PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}
