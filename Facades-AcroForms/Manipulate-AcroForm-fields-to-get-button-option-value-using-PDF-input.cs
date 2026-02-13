using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        // Paths to the input and output PDF files
        const string inputPath = "input.pdf";
        const string outputPath = "output.pdf";

        // Verify that the input file exists
        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Error: Input file not found – {inputPath}");
            return;
        }

        try
        {
            // Load the existing PDF document
            Document pdfDocument = new Document(inputPath);

            // Ensure the document contains a form
            if (pdfDocument.Form == null || pdfDocument.Form.Count == 0)
            {
                Console.WriteLine("The PDF does not contain any form fields.");
            }
            else
            {
                // Iterate through all form fields and process button fields
                foreach (Field field in pdfDocument.Form.Fields)
                {
                    if (field is ButtonField button)
                    {
                        // Retrieve the button's value (caption or export value)
                        var buttonValue = button.Value;
                        Console.WriteLine($"Button Field: {button.FullName}");
                        Console.WriteLine($"  Value: {buttonValue}");
                    }
                }
            }

            // Save the (unchanged) document using the provided save rule
            pdfDocument.Save(outputPath);
            Console.WriteLine($"Document saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}