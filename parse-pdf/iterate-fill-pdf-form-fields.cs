using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "output_filled.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each form field in the document
            foreach (Field field in doc.Form)
            {
                // Example processing based on field type
                if (field is TextBoxField txt)
                {
                    txt.Value = "Sample text";
                }
                else if (field is CheckboxField chk)
                {
                    chk.Checked = true;
                }
                else
                {
                    // For any other field types (e.g., radio button lists) set a generic value.
                    // This avoids referencing types that may not exist in the current Aspose.PDF version.
                    if (!string.IsNullOrEmpty(field.Value))
                    {
                        // Preserve existing value if needed
                    }
                    else
                    {
                        field.Value = "Option1"; // generic placeholder value
                    }
                }

                // Log the processed field
                Console.WriteLine($"Processed field: {field.FullName}, Value: {field.Value}");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
