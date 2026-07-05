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
            Field employeeField = doc.Form["EmployeeID"] as Field; // explicit cast from WidgetAnnotation
            if (employeeField != null)
            {
                // Make the field read‑only to prevent user modifications
                employeeField.ReadOnly = true;
            }
            else
            {
                Console.WriteLine("Field 'EmployeeID' not found in the document.");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved with read‑only 'EmployeeID' field to '{outputPath}'.");
    }
}
