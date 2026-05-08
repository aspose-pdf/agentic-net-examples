using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "flattened_output.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Load the PDF document inside a using block for deterministic disposal
        using (Document doc = new Document(inputPath))
        {
            // Ensure the document actually contains a form
            if (doc.Form != null)
            {
                // Retrieve the field named "Name" as a TextBoxField (or any Field that supports Value)
                if (doc.Form["Name"] is TextBoxField nameField)
                {
                    nameField.Value = "John Doe";
                }
                else if (doc.Form["Name"] is Field genericField && genericField != null)
                {
                    // Fallback for other field types that expose the Value property
                    genericField.Value = "John Doe";
                }
                else
                {
                    Console.Error.WriteLine("Field 'Name' not found or is of an unsupported type.");
                }

                // Flatten all form fields so they become part of the page content
                doc.Form.Flatten();
            }
            else
            {
                Console.Error.WriteLine("The PDF does not contain any form fields.");
            }

            // Save the resulting PDF
            doc.Save(outputPath);
        }

        Console.WriteLine($"Flattened PDF saved to '{outputPath}'.");
    }
}
