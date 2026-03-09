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
        const string fieldName = "myField"; // name of the form field to delete

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document
            using (Document doc = new Document(inputPath))
            {
                // Verify that the field exists before attempting deletion
                if (doc.Form.HasField(fieldName))
                {
                    // Delete the specified field by its name
                    doc.Form.Delete(fieldName);
                }
                else
                {
                    Console.WriteLine($"Field '{fieldName}' not found in the form.");
                }

                // Save the modified PDF; default PDF save is sufficient
                doc.Save(outputPath);
            }

            Console.WriteLine($"Field '{fieldName}' deleted. Saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}