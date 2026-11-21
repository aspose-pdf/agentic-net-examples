using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms; // for form field types

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "filled.pdf";
        const int expectedFormCount = 3; // adjust as needed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Document must be wrapped in a using block for deterministic disposal
            using (Document doc = new Document(inputPath))
            {
                // Verify the number of form fields
                int actualFormCount = doc.Form.Count;
                Console.WriteLine($"Form fields found: {actualFormCount}");

                if (actualFormCount != expectedFormCount)
                {
                    Console.Error.WriteLine($"Unexpected number of form fields. Expected {expectedFormCount}, but found {actualFormCount}.");
                    return;
                }

                // Example processing: set values for each form field
                foreach (Field field in doc.Form)
                {
                    // TextBox fields
                    if (field is TextBoxField txt)
                    {
                        txt.Value = "Sample text";
                    }
                    // CheckBox fields
                    else if (field is CheckboxField chk)
                    {
                        chk.Checked = true;
                    }
                    // Add handling for other field types as required
                }

                // Save the modified PDF
                doc.Save(outputPath);
                Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}