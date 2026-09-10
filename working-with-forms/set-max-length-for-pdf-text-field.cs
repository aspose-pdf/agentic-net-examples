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

        // Open the PDF document
        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field named "CustomerName" as a TextBoxField
            TextBoxField? textBox = doc.Form["CustomerName"] as TextBoxField;
            if (textBox == null)
            {
                Console.Error.WriteLine("Field 'CustomerName' not found or is not a text box.");
            }
            else
            {
                // Set the maximum character limit to 50
                textBox.MaxLen = 50;
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Updated PDF saved to '{outputPath}'.");
    }
}
