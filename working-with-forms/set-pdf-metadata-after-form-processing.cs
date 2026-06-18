using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Forms;

class Program
{
    static void Main()
    {
        const string inputPath  = "form_filled.pdf";
        const string outputPath = "final_with_metadata.pdf";

        if (!File.Exists(inputPath))
        {
            Console.Error.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        try
        {
            // Load the PDF that contains form fields
            using (Document doc = new Document(inputPath))
            {
                // OPTIONAL: process the form (e.g., flatten fields)
                if (doc.Form != null)
                {
                    doc.Form.Flatten();
                }

                // Set standard metadata properties
                doc.Info.Author = "John Doe";
                doc.Info.Title  = "Processed Form Document";

                // Alternative way to set the title (both are valid)
                doc.SetTitle("Processed Form Document");

                // Save the updated PDF
                doc.Save(outputPath);
            }

            Console.WriteLine($"Document saved with metadata to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}