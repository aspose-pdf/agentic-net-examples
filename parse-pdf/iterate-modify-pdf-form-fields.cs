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

        // Open the PDF inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Iterate over each form field in the document
            foreach (Field field in doc.Form.Fields)
            {
                // Output the field name (PartialName) and, if applicable, its current value
                Console.WriteLine($"Field: {field.PartialName}");

                // If the field is a text box, display and modify its value
                if (field is TextBoxField textBox)
                {
                    Console.WriteLine($"  Current Value: {textBox.Value}");
                    textBox.Value = "Sample text"; // set a new value
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Processed PDF saved to '{outputPath}'.");
    }
}
