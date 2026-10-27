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
        const int expectedFormCount = 5; // adjust as needed

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        // Ensure the Document is disposed properly
        using (Document doc = new Document(inputPath))
        {
            // Verify the number of form fields
            int actualCount = doc.Form.Count;
            Console.WriteLine($"Form fields found: {actualCount}");

            if (actualCount != expectedFormCount)
            {
                Console.Error.WriteLine($"Unexpected form field count. Expected {expectedFormCount}, but found {actualCount}.");
                // Abort further processing if the count is not as expected
                return;
            }

            // Example processing: set a default value for each text box field
            foreach (Field field in doc.Form)
            {
                if (field is TextBoxField txtField)
                {
                    txtField.Value = "Sample Text";
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}