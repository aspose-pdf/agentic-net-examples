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
        const string listBoxName = "myListBox"; // Name of the ListBoxField to delete

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"File not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF document inside a using block for proper disposal
            using (Document doc = new Document(inputPath))
            {
                // Verify that the form contains the specified field
                if (doc.Form.HasField(listBoxName))
                {
                    // Delete the ListBoxField by its name
                    doc.Form.Delete(listBoxName);
                }
                else
                {
                    Console.WriteLine($"ListBox field '{listBoxName}' not found in the document.");
                }

                // Save the modified PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"ListBox field deleted. Modified PDF saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}