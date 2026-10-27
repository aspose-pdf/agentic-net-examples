using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "template.pdf";
        const string outputPath = "filled_readonly.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form field named 'EmployeeID'
            var employeeField = doc.Form["EmployeeID"] as TextBoxField;
            if (employeeField != null)
            {
                // Set the field value (optional, based on generation logic)
                employeeField.Value = "12345";

                // Mark the field as read‑only to prevent user edits
                employeeField.ReadOnly = true;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"PDF saved to '{outputPath}'.");
    }
}