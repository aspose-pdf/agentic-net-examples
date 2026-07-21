using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
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
                else if (field is RadioButtonField rad)
                {
                    // Use the string value of the first option
                    if (rad.Options != null && rad.Options.Count > 0)
                        rad.Value = rad.Options[0].Value; // Fixed conversion
                }

                // Log field name and its current value
                Console.WriteLine($"Field: {field.FullName}, Value: {field.Value}");
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
