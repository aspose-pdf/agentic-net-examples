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
            Console.Error.WriteLine("Input file not found: " + inputPath);
            return;
        }

        using (Document doc = new Document(inputPath))
        {
            // Retrieve the field named "Score"
            Field field = doc.Form["Score"] as Field;
            if (field == null)
            {
                Console.Error.WriteLine("Field 'Score' not found in the PDF.");
            }
            else
            {
                // If the field is a NumberField, set allowed characters and maximum length
                NumberField numberField = field as NumberField;
                if (numberField != null)
                {
                    numberField.AllowedChars = "0123456789"; // only digits
                    numberField.MaxLen = 3; // allows values up to three digits (0‑100)
                }
                else
                {
                    // Fallback for a generic text box field
                    TextBoxField textBox = field as TextBoxField;
                    if (textBox != null)
                    {
                        textBox.MaxLen = 3;
                    }
                }
            }

            // Save the modified PDF
            doc.Save(outputPath);
        }

        Console.WriteLine("Field 'Score' configured and saved to " + outputPath);
    }
}