using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath = "input.pdf";
        const string outputPath = "rotated_label.pdf";
        const string fieldName = "LabelField"; // name of the form field to rotate

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Ensure the form exists and contains the specified field
                if (doc.Form != null && doc.Form[fieldName] != null)
                {
                    // Retrieve the form field (generic FormField)
                    var field = doc.Form[fieldName];

                    // Rotate the field's rectangle by 45 degrees
                    // Rectangle.Rotate(int angle) rotates the rectangle coordinates
                    field.Rect.Rotate(45);
                }
                else
                {
                    Console.WriteLine($"Form field '{fieldName}' not found.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Rotated label saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}