using System;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

class Program
{
    static void Main()
    {
        const string pdfPath = "input.pdf";
        const string outputPath = "fields.txt";

        if (!File.Exists(pdfPath))
        {
            Console.Error.WriteLine($"File not found: {pdfPath}");
            return;
        }

        try
        {
            // Load the PDF document
            Document doc = new Document(pdfPath);

            // Bind the document to the Form facade to work with form fields
            using (Form formFacade = new Form(doc))
            {
                // Retrieve all field names via the Form facade
                string[] fieldNames = formFacade.FieldNames ?? Array.Empty<string>();

                // Write the field names to a text file
                File.WriteAllLines(outputPath, fieldNames);
            }

            Console.WriteLine($"Field names saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
        }
    }
}
