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

        using (Document document = new Document(inputPath))
        {
            Form pdfForm = document.Form;
            // Locate the field named "TempField"
            Field targetField = null;
            foreach (Field field in pdfForm.Fields)
            {
                if (string.Equals(field.FullName, "TempField", StringComparison.OrdinalIgnoreCase) ||
                    string.Equals(field.Name, "TempField", StringComparison.OrdinalIgnoreCase))
                {
                    targetField = field;
                    break;
                }
            }

            if (targetField != null)
            {
                // Check whether the field contains any user‑entered value
                string fieldValue = targetField.Value != null ? targetField.Value.ToString() : string.Empty;
                if (string.IsNullOrEmpty(fieldValue))
                {
                    // No data – delete the field
                    pdfForm.Delete(targetField);
                    Console.WriteLine("TempField removed (no user data).");
                }
                else
                {
                    Console.WriteLine("TempField retained (contains data).");
                }
            }
            else
            {
                Console.WriteLine("TempField not found.");
            }

            document.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved as '{outputPath}'.");
    }
}