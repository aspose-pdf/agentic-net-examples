using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "input.pdf";
        const string outputPath = "processed.pdf";
        const int expectedFormCount = 3; // adjust as needed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for proper disposal
        using (Document doc = new Document(inputPath))
        {
            // Access the form object
            Form pdfForm = doc.Form;

            // Verify the number of form fields
            int actualCount = pdfForm.Count;
            Console.WriteLine($"Form fields found: {actualCount}");

            if (actualCount != expectedFormCount)
            {
                Console.Error.WriteLine($"Unexpected number of form fields. Expected: {expectedFormCount}, Actual: {actualCount}");
                // Optionally abort processing
                return;
            }

            // Example processing: set a value for the first field if it exists
            if (actualCount > 0)
            {
                // The Form collection implements IEnumerable<Field>
                foreach (Field field in pdfForm)
                {
                    // Example: set value for a TextBoxField
                    if (field is TextBoxField textBox)
                    {
                        textBox.Value = "Sample value";
                    }
                    // Process only the first field for this demo
                    break;
                }
            }

            // Save the modified document
            doc.Save(outputPath);
            Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
        }
    }
}
